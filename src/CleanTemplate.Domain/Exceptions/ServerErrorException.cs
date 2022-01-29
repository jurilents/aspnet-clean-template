using System.Net;

namespace CleanTemplate.Domain.Exceptions;

/// <summary>
/// Status Code: 500
/// </summary>
public class ServerErrorException : HttpException
{
	public ServerErrorException(string message = "InternalServerError")
			: base(HttpStatusCode.InternalServerError, message) { }
}