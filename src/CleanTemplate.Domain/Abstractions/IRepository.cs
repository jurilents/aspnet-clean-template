using System.Linq.Expressions;

namespace CleanTemplate.Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
	Task<TEntity?> GetByIdAsync<TKey>(TKey id, params string[] inclusions);
	Task<TEntity?> GetByIdAsync<TKey1, TKey2>(TKey1 id1, TKey2 id2, params string[] inclusions);
	Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter, params string[] inclusions);
	Task<IEnumerable<TEntity>> GetAllAsync(params string[] inclusions);
	Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filter, params string[] inclusions);
	Task<IEnumerable<TEntity>> FilterAsync(string? filter, string? sorting = null, int? skip = null, int? take = null, params string[] inclusions);

	Task<int> CountAsync();
	Task<int> CountAsync(string? filter);
	Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

	Task<long> CountLongAsync();
	Task<long> CountLongAsync(string? filter);
	Task<long> CountLongAsync(Expression<Func<TEntity, bool>> filter);

	void Create(TEntity entity);
	void Update(TEntity entity);
	void Delete(TEntity entity);

	Task SaveAsync(CancellationToken cancellationToken = default);
}