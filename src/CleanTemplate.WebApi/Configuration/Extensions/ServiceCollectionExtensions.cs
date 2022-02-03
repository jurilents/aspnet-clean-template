using CleanTemplate.Domain.Core.Tools;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.WebApi.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddCorsPolicies(this IServiceCollection services)
	{
		services.AddCors(o => o.AddPolicy("Cors", builder =>
		{
			builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
		}));
	}

	public static void AddCustomApiVersioning(this IServiceCollection services)
	{
		services.AddApiVersioning();

		services.AddVersionedApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'VVV";
			options.SubstituteApiVersionInUrl = true;
		});
	}

	public static void AddCustomSwagger(this IServiceCollection services)
	{
		services.ConfigureOptions<SwaggerConfigurationExtensions>();
		services.AddSwaggerGen();
	}

	public static void ConfigureApiBehaviorOptions(this IServiceCollection services)
	{
		services.Configure<ApiBehaviorOptions>(options =>
		{
			// Disable default ModelState validation (because we use only FluentValidation)
			options.SuppressModelStateInvalidFilter = true;
		});
	}

	public static void AddFactoryMiddleware(this IServiceCollection services)
	{
		IEnumerable<Type> middlewares = AssemblyProvider.GetImplementations<IMiddleware>();
		foreach (Type middleware in middlewares)
			services.AddScoped(middleware);
	}
}