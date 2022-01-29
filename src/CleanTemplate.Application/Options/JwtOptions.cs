using Microsoft.IdentityModel.Tokens;

namespace CleanTemplate.Application.Options;

public record JwtOptions
{
	public SecurityKey? Secret { get; set; }
	public int? AccessTokenLifetimeMinutes { get; set; }
}