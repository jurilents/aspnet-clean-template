using CleanTemplate.Data.SqlServer.Entities.Identity;
using CleanTemplate.Domain.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Data.SqlServer;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, long,
		AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>, IDatabaseContext
{
	public AppDbContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

		builder.SeedData();
	}
}