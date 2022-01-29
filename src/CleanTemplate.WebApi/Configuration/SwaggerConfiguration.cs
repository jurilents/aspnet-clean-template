using CleanTemplate.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanTemplate.WebApi.Configuration;

public class SwaggerConfigurationExtensions : IConfigureNamedOptions<SwaggerGenOptions>
{
	private readonly SwaggerConfigurationSettings _settings;
	private readonly IApiVersionDescriptionProvider _provider;

	public SwaggerConfigurationExtensions(IConfiguration configuration, IApiVersionDescriptionProvider provider)
	{
		_settings = configuration.GetSwaggerSettings();
		_provider = provider;
	}

	public void Configure(SwaggerGenOptions options)
	{
		// add swagger document for every API version discovered
		foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
			options.SwaggerDoc(description.GroupName.ToLower(), CreateVersionInfo(description));

		options.CustomSchemaIds(NameSchemaIdSelector);

		foreach (string xmlPath in GetXmlComments())
			options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
	}

	public void Configure(string name, SwaggerGenOptions options) => Configure(options);


	private OpenApiInfo CreateVersionInfo(ApiVersionDescription versionDescription)
	{
		return new OpenApiInfo
		{
			Title = _settings.Title,
			Version = versionDescription.ApiVersion.ToString(),
			Description = _settings.Description,
		};
	}

	private IEnumerable<string> GetXmlComments()
	{
		// Set the comments path for the Swagger JSON and UI
		if (_settings.IncludeComments is { Length: > 0 })
		{
			foreach (string xmlDocsFile in _settings.IncludeComments)
				yield return Path.Combine(AppContext.BaseDirectory, xmlDocsFile);
		}
	}

	private static string NameSchemaIdSelector(Type type) =>
			type.FullName!
					.Replace("Microsoft.AspNetCore.Mvc.", "Microsoft.")
					.Replace("CleanTemplate.Application.Features.", "")
					.Replace("CleanTemplate.Application.Models.", "")
					.Replace("CleanTemplate.WebApi.Core.", "");
}