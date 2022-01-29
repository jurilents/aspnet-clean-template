using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanTemplate.Application.Constrains;

public struct ApplicationClaims
{
	public const string Id = JwtRegisteredClaimNames.Sub;
	public const string UserName = JwtRegisteredClaimNames.UniqueName;
	public const string Role = ClaimTypes.Role;
}