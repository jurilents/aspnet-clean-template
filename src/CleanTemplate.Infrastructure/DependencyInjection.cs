using CleanTemplate.Data.SqlServer;
using CleanTemplate.Data.SqlServer.Entities.Identity;
using CleanTemplate.Domain.Abstractions;
using CleanTemplate.Domain.Core.Tools;
using CleanTemplate.Domain.Extensions;
using CleanTemplate.Infrastructure.Core;
using CleanTemplate.Infrastructure.Repositories;
using CleanTemplate.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTemplate.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services)
	{
		services.AddInfrastructureServices();
		services.AddRepositories();
	}
	
	
	public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddIdentity<AppUser, AppRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddTokenProvider("Default", typeof(EmailTwoFactorAuthentication));

		services.Configure<IdentityOptions>(configuration.GetSection("Identity"));
		services.Configure<PasswordHasherOptions>(option => option.IterationCount = 7000);
	}

	private static void AddInfrastructureServices(this IServiceCollection services)
	{
		IEnumerable<Type> serviceTypes = AssemblyProvider.GetTypes("CleanTemplate.Infrastructure");

		foreach (Type implType in serviceTypes)
		{
			var attr = implType.GetAttribute<InjectAttribute>();
			if (attr is null)
				continue;

			switch (attr.InjectionType)
			{
				case InjectionType.Interface:
				{
					attr.ServiceType ??= implType.GetInterfaces().First();
					services.Add(new ServiceDescriptor(attr.ServiceType, implType, attr.Lifetime));
					break;
				}
				case InjectionType.Self:
				{
					services.Add(new ServiceDescriptor(implType, implType, attr.Lifetime));
					break;
				}
				case InjectionType.BaseClass:
				{
					services.Add(new ServiceDescriptor(implType.BaseType!, implType, attr.Lifetime));
					break;
				}
				default:
					throw new ArgumentOutOfRangeException(nameof(attr.InjectionType), "Invalid injection type");
			}

			Type[] interfaces = implType.GetInterfaces();
			services.Add(new ServiceDescriptor(interfaces.First(), implType, attr.Lifetime));
		}
	}

	private static void AddRepositories(this IServiceCollection services)
	{
		services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
	}
}