namespace Tacles.Core.Constrains;

/// <summary>
/// Contains common entity length-param constants
/// </summary>
public static class Limits
{
	/// <summary>
	/// Maximum length for: first name, last name, personal name, etc
	/// </summary>
	public const int MaxNameLength = 50;

	/// <summary>
	/// Minimum length for: first name, last name, personal name, etc
	/// </summary>
	public const int MinNameLength = 1;

	/// <summary>
	/// Maximum length for: route path (/article-name-321)
	/// </summary>
	public const int MaxPathLength = 100;

	/// <summary>
	/// Maximum length for: post resource caption, article resource caption etc
	/// </summary>
	public const int MaxCaptionLength = 200;

	/// <summary>
	/// Max length for: article title
	/// </summary>
	public const int MaxTitleLength = 200;
}