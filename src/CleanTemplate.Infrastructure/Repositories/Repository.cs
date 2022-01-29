using System.Linq.Expressions;
using CleanTemplate.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanTemplate.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : class, IEntity
{
	protected readonly IDatabaseContext Context;
	protected readonly DbSet<TEntity> Db;

	public Repository(IDatabaseContext databaseContext)
	{
		Context = databaseContext;
		Db = databaseContext.Set<TEntity>();
	}


	public virtual async Task<TEntity?> GetByIdAsync<TKey>(TKey id, params string[] inclusions)
	{
		TEntity? entity = await Db.FindAsync(id);
		return await AddInclusionsAsync(entity, inclusions);
	}

	public virtual async Task<TEntity?> GetByIdAsync<TKey1, TKey2>(TKey1 id1, TKey2 id2, params string[] inclusions)
	{
		TEntity? entity = await Db.FindAsync(id1, id2);
		return await AddInclusionsAsync(entity, inclusions);
	}

	public virtual async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter, params string[] inclusions)
	{
		return await Db
				.AsNoTracking()
				.Where(filter)
				.ApplyInclusions(inclusions)
				.SingleOrDefaultAsync();
	}

	public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] inclusions)
	{
		return await Db
				.AsNoTracking()
				.ApplyInclusions(inclusions)
				.ToListAsync();
	}

	public virtual async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filter, params string[] inclusions)
	{
		return await Db
				.AsNoTracking()
				.Where(filter)
				.ApplyInclusions(inclusions)
				.ToListAsync();
	}

	public virtual async Task<IEnumerable<TEntity>> FilterAsync(string? filter, string? sorting, int? skip, int? take, params string[] inclusions)
	{
		return await Db
				.AsNoTracking()
				.ApplyFiltering(filter)
				.ApplyPaging(skip, take)
				.ApplySorting(sorting)
				.ApplyInclusions(inclusions)
				.ToListAsync();
	}

	public virtual async Task<int> CountAsync() => await Db.CountAsync();
	public virtual async Task<int> CountAsync(string? filter) => await Db.ApplyFiltering(filter).CountAsync();
	public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter) => await Db.Where(filter).CountAsync();

	public virtual async Task<long> CountLongAsync() => await Db.LongCountAsync();
	public virtual async Task<long> CountLongAsync(string? filter) => await Db.ApplyFiltering(filter).LongCountAsync();
	public virtual async Task<long> CountLongAsync(Expression<Func<TEntity, bool>> filter) => await Db.Where(filter).LongCountAsync();

	public virtual void Create(TEntity entity) => Db.Add(entity);
	public virtual void Update(TEntity entity) => Db.Update(entity);
	public virtual void Delete(TEntity entity) => Db.Remove(entity);

	public async Task SaveAsync(CancellationToken cancellationToken = default) => await Context.SaveChangesAsync(cancellationToken);


	private async Task<TEntity?> AddInclusionsAsync(TEntity? entity, string[] inclusions)
	{
		if (entity is not null && inclusions.Length > 0)
		{
			EntityEntry entityEntry = Context.Entry(entity);

			IEnumerable<Task> tasks = inclusions.Select(inclusion => entityEntry.Reference(inclusion).LoadAsync());
			await Task.WhenAll(tasks);
		}

		return entity;
	}
}