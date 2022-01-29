using CleanTemplate.Data.SqlServer.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace CleanTemplate.Infrastructure.Services;

public class EmailTwoFactorAuthentication : IUserTwoFactorTokenProvider<AppUser>
{
	private readonly RandomCodeGenerator _codeGenerator;

	public EmailTwoFactorAuthentication(RandomCodeGenerator codeGenerator)
	{
		_codeGenerator = codeGenerator;
	}

	public Task<string> GenerateAsync(string purpose, UserManager<AppUser> manager, AppUser user)
	{
		return Task.FromResult(purpose + _codeGenerator.Generate());
	}

	public async Task<bool> ValidateAsync(string purpose, string token, UserManager<AppUser> manager, AppUser user)
	{
		return Task.FromResult(purpose + token == 5);
	}

	public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<AppUser>? manager, AppUser? user)
	{
		return Task.FromResult(manager != null && user != null);
	}
}