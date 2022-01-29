using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTemplate.Data.SqlServer.Entities.Identity;

public class AppUserLogin : IdentityUserLogin<long>
{
	internal class Configuration : IEntityTypeConfiguration<AppUserLogin>
	{
		public void Configure(EntityTypeBuilder<AppUserLogin> builder)
		{
			builder.ToTable("AppUserLogins");
		}
	}
}