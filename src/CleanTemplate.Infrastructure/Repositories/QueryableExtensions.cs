using System.Linq.Dynamic.Core;
using CleanTemplate.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Infrastructure.Repositories;

public static class QueryableExtensions
{
	public static IQueryable<TEntity> ApplyFiltering<TEntity>(this IQueryable<TEntity> queryable, string? filter)
			where TEntity : class, IEntity
	{
		if (string.IsNullOrEmpty(filter))
			return queryable;

		return queryable.Where(filter);
	}

	public static IQueryable<TEntity> ApplySorting<TEntity>(this IQueryable<TEntity> queryable, string? sorting)
			where TEntity : class, IEntity
	{
		if (string.IsNullOrEmpty(sorting))
			return queryable;

		return sorting.Split(',').Aggregate(queryable, (current, sortingCase) => current.OrderBy(sortingCase));
	}

	public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> queryable, int? skip, int? take)
			where TEntity : class, IEntity
	{
		if (skip is not null && take is not null)
			return queryable.Skip((int) skip).Take((int) take);

		return queryable;
	}

	public static IQueryable<TEntity> ApplyInclusions<TEntity>(this IQueryable<TEntity> queryable, string[] inclusions)
			where TEntity : class, IEntity
	{
		if (inclusions.Length <= 0)
			return queryable;

		foreach (string inclusion in inclusions)
			queryable.Include(inclusion);

		return queryable;
	}
}