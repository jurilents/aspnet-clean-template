using CleanTemplate.Data.SqlServer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Data.SqlServer;

public static class DataSeed
{
	public static void SeedData(this ModelBuilder builder)
	{
		long role1Id = builder.SeedRole("master");
		long role2Id = builder.SeedRole("slave");

		long user1Id = builder.SeedUser("jurilents", "juri@tacles.net", "abcX0102");
		long user2Id = builder.SeedUser("tweeker", "tweeker@tacles.net", "abcX0102");

		builder.SeedUserRole(user1Id, role1Id);
		builder.SeedUserRole(user2Id, role2Id);
	}


	private static long SeedUser(this ModelBuilder builder, string username, string email, string password)
	{
		builder.Entity<AppUser>().HasData(new AppUser
		{
			Id = userIdInc,
			UserName = username,
			NormalizedUserName = username.ToUpper(),
			Email = email,
			NormalizedEmail = email.ToUpper(),
			PasswordHash = new PasswordHasher<AppUser>().HashPassword(null!, password)
		});

		return userIdInc++;
	}

	private static long SeedRole(this ModelBuilder builder, string name)
	{
		builder.Entity<AppRole>().HasData(new AppRole
		{
			Id = roleIdInc,
			Name = name,
			NormalizedName = name.ToUpper()
		});

		return roleIdInc++;
	}

	private static void SeedUserRole(this ModelBuilder builder, long userId, long roleId)
	{
		builder.Entity<AppUserRole>().HasData(new AppUserRole
		{
			UserId = userId,
			RoleId = roleId
		});
	}


	private static long userIdInc = 1;
	private static long roleIdInc = 1;
}