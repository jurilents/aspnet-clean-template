namespace CleanTemplate.Domain;

public record Error(string Code, IEnumerable<Error.Details>? Errors)
{
	public record Details(string Code, string Message);
}