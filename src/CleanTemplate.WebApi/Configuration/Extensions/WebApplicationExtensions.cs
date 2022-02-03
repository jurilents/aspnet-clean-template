namespace CleanTemplate.WebApi.Configuration.Extensions;

public static class WebApplicationExtensions
{
	public static void Redirect(this WebApplication app, string from, string to)
	{
		app.MapGet(from, context =>
		{
			context.Response.Redirect(to, true);
			return Task.CompletedTask;
		});
	}
}