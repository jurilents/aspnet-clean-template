using CleanTemplate.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.WebApi.Core;

[ApiController]
[Route("/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(Error), 400)]
public class ApiController : ControllerBase
{
	private IMediator? _mediator;

	protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}