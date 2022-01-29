using System.Net;

namespace CleanTemplate.Domain.Exceptions;

/// <summary>
/// Status Code: 403
/// </summary>
public class ForbidException : HttpException
{
	public ForbidException(string message = "Forbid")
			: base(HttpStatusCode.Forbidden, message) { }
}