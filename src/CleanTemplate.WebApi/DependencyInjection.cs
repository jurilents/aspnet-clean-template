using CleanTemplate.Application.DependencyInjection;
using CleanTemplate.Data.SqlServer.DependencyInjection;
using CleanTemplate.Infrastructure.DependencyInjection;
using CleanTemplate.WebApi.Configuration;
using CleanTemplate.WebApi.Configuration.Extensions;

namespace CleanTemplate.WebApi;

public static class DependencyInjection
{
	public static void AddDatabase(this WebApplicationBuilder builder)
	{
		builder.Services.AddSqlServerDatabase(builder.Configuration);
		builder.Services.AddIdentityServices(builder.Configuration);
	}

	public static void AddApplication(this WebApplicationBuilder builder)
	{
		builder.Services.AddApplication(builder.Configuration);
	}

	public static void AddInfrastructure(this WebApplicationBuilder builder)
	{
		builder.Services.AddInfrastructure();
	}

	public static void AddWebApi(this WebApplicationBuilder builder)
	{
		builder.Services.AddFactoryMiddleware();

		builder.Services.AddControllers(KebabCaseNamingConvention.Use)
				.ConfigureJsonSerializerConventions();

		builder.Services.ConfigureApiBehaviorOptions();
		builder.Services.AddCustomApiVersioning();

		if (builder.Configuration.GetSwaggerSettings().Enabled)
		{
			builder.Services.AddCustomSwagger();
		}
	}
}