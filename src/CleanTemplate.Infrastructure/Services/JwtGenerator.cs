using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CleanTemplate.Application.Constrains;
using CleanTemplate.Application.Options;
using CleanTemplate.Application.Services;
using CleanTemplate.Data.SqlServer.Entities.Identity;
using CleanTemplate.Infrastructure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanTemplate.Infrastructure.Services;

[Inject(ServiceLifetime.Transient)]
public class JwtGenerator : IJwtGenerator
{
	private readonly UserManager<AppUser> _userManager;
	private readonly JwtOptions _options;

	public JwtGenerator(IOptions<JwtOptions> optionsAccessor, UserManager<AppUser> userManager)
	{
		_userManager = userManager;
		_options = optionsAccessor.Value;
	}

	public async Task<string> GenerateAsync(AppUser user)
	{
		DateTime expires = DateTime.UtcNow.AddMinutes(_options.AccessTokenLifetimeMinutes ?? 10);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(await GetUserClaimsAsync(user)),
			Expires = expires,
			SigningCredentials = new SigningCredentials(_options.Secret, SecurityAlgorithms.HmacSha256Signature),
			IssuedAt = DateTime.Now,
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		SecurityToken jwt = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(jwt);
	}


	private async Task<IEnumerable<Claim>> GetUserClaimsAsync(AppUser user)
	{
		List<Claim> claims = new()
		{
			new Claim(ApplicationClaims.Id, user.Id.ToString("00000000")),
			new Claim(ApplicationClaims.UserName, user.NormalizedUserName),
		};

		IEnumerable<string> roles = await _userManager.GetRolesAsync(user);
		claims.AddRange(roles.Select(role => new Claim(ApplicationClaims.Role, role)));

		return claims;
	}
}