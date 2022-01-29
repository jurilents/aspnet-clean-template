using CleanTemplate.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTemplate.Data.SqlServer.Entities.Identity;

public class AppUser : IdentityUser<long>, IEntity
{
	internal class Configuration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.ToTable("AppUsers");
		}
	}
}