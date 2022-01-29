using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTemplate.Data.SqlServer.Entities.Identity;

public class AppUserClaim : IdentityUserClaim<long>
{
	internal class Configuration : IEntityTypeConfiguration<AppUserClaim>
	{
		public void Configure(EntityTypeBuilder<AppUserClaim> builder)
		{
			builder.ToTable("AppUserClaims");
		}
	}
}