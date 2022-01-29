using System.Net;

namespace CleanTemplate.Domain.Exceptions;

public class HttpException : Exception
{
	public HttpStatusCode StatusCode { get; }

	public HttpException(HttpStatusCode statusCode, string message = "")
			: base(message)
	{
		StatusCode = statusCode;
	}

	public HttpException(int statusCode, string message = "")
			: base(message)
	{
		StatusCode = (HttpStatusCode) statusCode;
	}
}