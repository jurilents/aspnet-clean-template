using System.Net;
using System.Text.Json;
using CleanTemplate.Domain;
using CleanTemplate.Domain.Exceptions;
using FluentValidation;

namespace CleanTemplate.WebApi.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
	private readonly ILogger _logger;

	public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
	{
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (ValidationException e)
		{
			Error error = new("ValidationFailed", e.Errors
					.Select(ve => new Error.Details(ve.PropertyName, ve.ErrorMessage)).ToArray());
			await WriteJsonResponseAsync(context, HttpStatusCode.BadRequest, error);
		}
		catch (ValidationFailedException e)
		{
			await WriteJsonResponseAsync(context, HttpStatusCode.BadRequest, e.Error);
		}
		catch (HttpException e)
		{
			if ((int) e.StatusCode >= 500)
				await Write500StatusCodeResponseAsync(context, e);
			else
				await WriteStatusCodeResponseAsync(context, e.StatusCode);
		}
		catch (Exception e)
		{
			await Write500StatusCodeResponseAsync(context, e);
		}
	}


	private async Task WriteStatusCodeResponseAsync(HttpContext context, HttpStatusCode statusCode)
	{
		context.Response.StatusCode = (int) statusCode;
		await context.Response.CompleteAsync();
	}

	private async Task WriteJsonResponseAsync<T>(HttpContext context, HttpStatusCode statusCode, T response)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int) statusCode;
		await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonConventions.CamelCase));
	}

	private async Task Write500StatusCodeResponseAsync(HttpContext context, Exception exception)
	{
		string errorUid = GenerateErrorUid();
		_logger.LogError(exception, "Internal Server Error {Uid}", errorUid);

		context.Response.ContentType = "text/plain";
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;

		await context.Response.WriteAsync($"===== SERVER ERROR =====\nSTAMP: {errorUid}\n{exception}\n===== ===== ===== =====");
	}

	private string GenerateErrorUid() => Guid.NewGuid().ToString("N")[..5];
}