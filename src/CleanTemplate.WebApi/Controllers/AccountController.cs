using CleanTemplate.Application.Features.Account;
using CleanTemplate.WebApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.WebApi.Controllers;

[ApiVersion(Versions.V1)]
public class AccountController : ApiController
{
	[AllowAnonymous]
	// [ValidateAntiForgeryToken]
	[HttpPost("register")]
	public async Task<AuthResult> RegisterAsync([FromBody] RegisterCommand command)
	{
		return await Mediator.Send(command);
	}

	[AllowAnonymous]
	// [ValidateAntiForgeryToken]
	[HttpPost("login")]
	public async Task<AuthResult> LoginAsync([FromBody] LoginCommand command)
	{
		return await Mediator.Send(command);
	}

	[HttpPost("logout")]
	public async Task<StatusCodeResult> LogoutAsync()
	{
		await Mediator.Send(new LogoutQuery());
		return Ok();
	}

	[HttpPost("confirm-email")]
	public async Task<StatusCodeResult> ConfirmEmailAsync(ConfirmEmailQuery query)
	{
		await Mediator.Send(query);
		return Ok();
	}
}