using System.Reflection;
using System.Text;
using CleanTemplate.Application.Options;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanTemplate.Application.DependencyInjection;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.BindConfigurationOptions(configuration);

		services.AddAppMediatR();
	}

	private static void BindConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtOptions>(options =>
		{
			options.Secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));
			options.AccessTokenLifetimeMinutes = int.Parse(configuration["Jwt:AccessTokenLifetimeMinutes"]);
		});
	}

	private static void AddAppMediatR(this IServiceCollection services)
	{
		var assembly = new[] { Assembly.GetExecutingAssembly() };
		services.AddFluentValidation(assembly);
		services.AddMediatR(assembly);
	}
}