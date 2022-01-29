using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanTemplate.WebApi.Extensions;

public static class MvcBuilderExtensions
{
	public static IMvcBuilder ConfigureJsonSerializerConventions(this IMvcBuilder builder)
	{
		return builder.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)));
	}
}