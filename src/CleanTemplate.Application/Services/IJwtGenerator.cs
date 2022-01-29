using CleanTemplate.Data.SqlServer.Entities.Identity;

namespace CleanTemplate.Application.Services;

public interface IJwtGenerator
{
	Task<string> GenerateAsync(AppUser user);
}