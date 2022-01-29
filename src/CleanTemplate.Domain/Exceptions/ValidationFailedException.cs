using System.Net;

namespace CleanTemplate.Domain.Exceptions;

/// <summary>
/// Status Code: 400
/// </summary>
public class ValidationFailedException : HttpException
{
	public IEnumerable<Error.Details>? Details { get; }

	public Error Error => new(Message, Details?.ToArray());


	public ValidationFailedException(IEnumerable<Error.Details>? details = null)
			: base(HttpStatusCode.BadRequest, "ValidationFailed") => Details = details;

	public ValidationFailedException(string message, IEnumerable<Error.Details>? details = null)
			: base(HttpStatusCode.BadRequest, message) => Details = details;
}