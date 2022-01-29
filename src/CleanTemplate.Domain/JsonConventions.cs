using System.Text.Json;

namespace CleanTemplate.Domain;

public static class JsonConventions
{
	public static readonly JsonSerializerOptions CamelCase = new()
	{
		// DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	};

	public static readonly JsonSerializerOptions ExtendedScheme = new()
	{
		ReadCommentHandling = JsonCommentHandling.Skip
	};
}