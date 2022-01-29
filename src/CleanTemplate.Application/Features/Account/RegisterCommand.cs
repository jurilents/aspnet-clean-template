using CleanTemplate.Application.Options;
using CleanTemplate.Application.Services;
using CleanTemplate.Data.SqlServer.Entities.Identity;
using CleanTemplate.Domain;
using CleanTemplate.Domain.Exceptions;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CleanTemplate.Application.Features.Account;

public class RegisterCommand : IRequest<AuthResult>
{
	public string Email { get; init; } = default!;
	public string UserName { get; init; } = default!;

	public string Password { get; init; } = default!;
	public string PasswordConfirm { get; init; } = default!;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
{
	private readonly WebOptions _options;
	private readonly IEmailSender _emailSender;
	private readonly IJwtGenerator _jwtGenerator;
	private readonly UserManager<AppUser> _userManager;

	public RegisterCommandHandler(IOptions<WebOptions> optionsAccessor, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator, IEmailSender emailSender)
	{
		_userManager = userManager;
		_emailSender = emailSender;
		_jwtGenerator = jwtGenerator;
		_options = optionsAccessor.Value;
	}

	public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		var user = request.Adapt<AppUser>();

		var result = await _userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
			throw new ValidationFailedException("RegistrationFailed", result.Errors.Select(e => new Error.Details(e.Code, e.Description)));

		string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		string confirmationLink = string.Format(_options.EmailConfirmationUrl, user.Id, confirmationToken);

		await _emailSender.SendAsync(request.Email, "Confirm your email", $"Confirm: <a href=\"{confirmationLink}\">YES</a> or <i>no</i>");

		string accessToken = await _jwtGenerator.GenerateAsync(user);
		return new AuthResult(user.UserName, accessToken);
	}
}

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
	public RegisterCommandValidator()
	{
		RuleFor(o => o.Email).NotEmpty().EmailAddress();
		RuleFor(o => o.UserName).NotEmpty();
		RuleFor(o => o.Password).NotEmpty().Equal(o => o.PasswordConfirm);
	}
}