namespace CleanTemplate.Application.Options;

public class WebOptions
{
	public string ApiUrl { get; init; } = default!;
	public string EmailConfirmationRoute { get; init; } = default!;


	public string EmailConfirmationUrl => ApiUrl + EmailConfirmationRoute;
}