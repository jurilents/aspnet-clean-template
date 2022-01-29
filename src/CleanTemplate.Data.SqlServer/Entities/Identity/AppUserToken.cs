using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTemplate.Data.SqlServer.Entities.Identity;

public class AppUserToken : IdentityUserToken<long>
{
	internal class Configuration : IEntityTypeConfiguration<AppUserToken>
	{
		public void Configure(EntityTypeBuilder<AppUserToken> builder)
		{
			builder.ToTable("AppUserTokens");
		}
	}
}