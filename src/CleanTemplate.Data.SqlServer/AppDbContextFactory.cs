using CleanTemplate.Data.SqlServer.Core;

namespace CleanTemplate.Data.SqlServer;

public class AppDbContextFactory : SqlServerDbContextFactory<AppDbContext>
{
	public override string SelectedConnectionName => "LocalSqlServer";
	public override string ConnectionString => GetConnectionStringsFromJson("../CleanTemplate.WebApi/appsettings.json").ConnectionStrings![SelectedConnectionName];
}