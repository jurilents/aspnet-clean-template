namespace CleanTemplate.WebApi.Configuration;

public class SwaggerConfigurationSettings
{
	public bool Enabled { get; init; }
	public string? Title { get; init; }
	public string? Description { get; init; }
	public string[]? IncludeComments { get; init; }
}