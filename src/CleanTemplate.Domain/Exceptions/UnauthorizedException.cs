using System.Net;

namespace CleanTemplate.Domain.Exceptions;

/// <summary>
/// Status Code: 401
/// </summary>
public class UnauthorizedException : HttpException
{
	public UnauthorizedException(string message = "Unauthorized")
			: base(HttpStatusCode.Unauthorized, message) { }
}