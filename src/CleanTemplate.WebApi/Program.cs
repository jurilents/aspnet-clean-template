using CleanTemplate.WebApi.Extensions;

namespace CleanTemplate.WebApi;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		ConfigureBuilder(builder);

		var app = builder.Build();
		ConfigureWebApp(app);

		app.Run();
	}

	private static void ConfigureBuilder(WebApplicationBuilder builder)
	{
		builder.AddDatabase();
		builder.AddApplication();
		builder.AddInfrastructure();
		builder.AddWebApi();
	}

	private static void ConfigureWebApp(WebApplication app)
	{
		if (app.Configuration.GetSwaggerSettings().Enabled)
		{
			app.UseCustomSwagger();
			app.Redirect("/", "/swagger");
		}

		if (app.Environment.IsProduction())
		{
			app.UseHttpsRedirection();
		}

		app.UseCustomExceptionHandler();

		app.UseAuthorization();

		app.MapControllers();
	}
}