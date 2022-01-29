using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CleanTemplate.WebApi.Configuration;

/// <summary>
/// Kebab case routes naming convention
/// </summary>
/// <remarks>
/// kebab-case-example
/// </remarks>
public class KebabCaseNamingConvention : IOutboundParameterTransformer
{
	public static void Use(MvcOptions options)
	{
		options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseNamingConvention()));
	}

	public string? TransformOutbound(object? value)
	{
		return value is null ? null : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
	}
}