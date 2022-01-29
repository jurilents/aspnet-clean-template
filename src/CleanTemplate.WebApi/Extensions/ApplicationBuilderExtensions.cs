using CleanTemplate.WebApi.Middleware;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CleanTemplate.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
	public static void UseCustomExceptionHandler(this IApplicationBuilder app)
	{
		app.UseMiddleware<ExceptionHandlerMiddleware>();
	}

	public static void UseCustomSwagger(this IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(options =>
		{
			var swaggerSettings = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSwaggerSettings();
			var apiProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

			foreach (ApiVersionDescription description in apiProvider.ApiVersionDescriptions)
			{
				string name = $"{swaggerSettings.Title} {description.GroupName.ToUpper()}";
				string url = $"/swagger/{description.GroupName}/swagger.json";
				options.SwaggerEndpoint(url, name);
			}
		});
	}
}