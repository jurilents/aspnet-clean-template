using CleanTemplate.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Data.SqlServer.Core;

public class SqlServerDbContextFactory<TContext> : DbContextFactoryBase<TContext>
		where TContext : DbContext, IDatabaseContext
{
	public override DbContextOptions CreateContextOptions()
	{
		Console.WriteLine("Database connection used: " + SelectedConnectionName);

		var optionsBuilder = new DbContextOptionsBuilder<TContext>();

		optionsBuilder.UseSqlServer(ConnectionString,
				options => options.MigrationsAssembly(MigrationsAssembly));

		return optionsBuilder.Options;
	}
}