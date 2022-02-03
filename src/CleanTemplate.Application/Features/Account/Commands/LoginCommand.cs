using CleanTemplate.Application.Services;
using CleanTemplate.Data.SqlServer.Entities.Identity;
using CleanTemplate.Domain;
using CleanTemplate.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanTemplate.Application.Features.Account;

public class LoginCommand : IRequest<AuthResult>
{
	public string Login { get; set; } = default!;
	public string Password { get; set; } = default!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
{
	private readonly IJwtGenerator _jwtGenerator;
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;

	public LoginCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
	{
		_userManager = userManager;
		_jwtGenerator = jwtGenerator;
		_signInManager = signInManager;
	}

	public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		var user = await FindAppUserByLogin(request);
		var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
		string accessToken = await _jwtGenerator.GenerateAsync(user);

		if (result.Succeeded)
			return new AuthResult(user.UserName, accessToken);

		throw new ValidationFailedException(invalidUsernameOrPasswordError);
	}

	private async Task<AppUser> FindAppUserByLogin(LoginCommand request)
	{
		var user = await _userManager.FindByEmailAsync(request.Login);
		user ??= await _userManager.FindByNameAsync(request.Login);
		return user ?? throw new ValidationFailedException(invalidUsernameOrPasswordError);
	}

	private static readonly Error.Details[] invalidUsernameOrPasswordError = { new("InvalidUsernameOrPassword", "Invalid username or password") };
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
	public LoginCommandValidator()
	{
		RuleFor(o => o.Login).NotEmpty();
		RuleFor(o => o.Password).NotEmpty();
	}
}