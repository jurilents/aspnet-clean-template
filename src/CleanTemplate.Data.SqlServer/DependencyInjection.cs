using CleanTemplate.Domain.Abstractions;
using CleanTemplate.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTemplate.Data.SqlServer.DependencyInjection;

public static class DependencyInjection
{
	public static void AddSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext(configuration);
	}

	private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		if (configuration.IsTesting())
			return;

		var contextFactory = new AppDbContextFactory();
		string connectionString = configuration.GetConnectionString(contextFactory.SelectedConnectionName);

		services.AddDbContext<AppDbContext>(cob =>
		{
			cob.UseSqlServer(connectionString,
					opts => opts.MigrationsAssembly(contextFactory.MigrationsAssembly));
		});

		services.AddScoped<IDatabaseContext, AppDbContext>();
	}
}