using System.Net;

namespace CleanTemplate.Domain.Exceptions;

public class NotFoundException : HttpException
{
	public NotFoundException(string message = "NotFound")
			: base(HttpStatusCode.NotFound, message) { }
}