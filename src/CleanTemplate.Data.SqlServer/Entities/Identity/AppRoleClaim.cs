using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTemplate.Data.SqlServer.Entities.Identity;

public class AppRoleClaim : IdentityRoleClaim<long>
{
	internal class Configuration : IEntityTypeConfiguration<AppRoleClaim>
	{
		public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
		{
			builder.ToTable("AppRoleClaims");
		}
	}
}