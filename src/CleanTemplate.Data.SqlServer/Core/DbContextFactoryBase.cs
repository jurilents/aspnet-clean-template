using System.Reflection;
using System.Text.Json;
using CleanTemplate.Domain;
using CleanTemplate.Domain.Core;
using CleanTemplate.Domain.Core.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanTemplate.Data.SqlServer.Core;

public abstract class DbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
		where TContext : DbContext
{
	public DbContextFactoryBase()
	{
		AssemblyProvider.AddAssembly(GetType().Assembly);
	}


	/// <summary>
	/// The name of the configuration property
	/// that contains the actual connection string to use
	/// (in section ConnectionStrings)
	/// </summary>
	public virtual string SelectedConnectionName => "DefaultConnection";

	/// <summary>
	/// Connection string (ready to use)
	/// </summary>
	public virtual string ConnectionString
	{
		get
		{
			ConnectionStringsSettings settings = GetConnectionStringsFromJson();
			if (settings.ConnectionStrings!.ContainsKey(SelectedConnectionName))
				return settings.ConnectionStrings[SelectedConnectionName];

			throw new KeyNotFoundException($"The connection string '{SelectedConnectionName}' is not presented in configuration.");
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual string MigrationsAssembly => this.GetType().Assembly.GetName().Name!;

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public abstract DbContextOptions CreateContextOptions();


	/// <summary>
	/// 
	/// </summary>
	/// <param name="appsettingsPath"></param>
	/// <returns></returns>
	/// <exception cref="FileNotFoundException"></exception>
	protected virtual ConnectionStringsSettings GetConnectionStringsFromJson(string appsettingsPath = "appsettings.json")
	{
		ConnectionStringsSettings settings;

		string absPath = Path.Combine(Directory.GetCurrentDirectory(), appsettingsPath);

		if (File.Exists(absPath))
		{
			string json = File.ReadAllText(absPath);
			settings = JsonSerializer.Deserialize<ConnectionStringsSettings>(json, JsonConventions.ExtendedScheme)!;
		}
		else
			throw new FileNotFoundException($"Config file not found: {appsettingsPath}");

		return settings;
	}

	public TContext CreateDbContext(string[] args)
	{
		Type contextType = typeof(TContext);
		ConstructorInfo? ctor = contextType.GetConstructor(new[] { typeof(DbContextOptions) });

		if (ctor is null)
			throw new TypeLoadException($"Database context {contextType.Name} has no constructor with {nameof(DbContextOptions)} argument");

		return (TContext) ctor.Invoke(new object[] { CreateContextOptions() });
	}
}