using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanTemplate.Domain.Abstractions;

public interface IDatabaseContext
{
	DbSet<TEntity> Set<TEntity>() where TEntity : class;
	EntityEntry Entry(object entity);
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	int SaveChanges();


	virtual string EntitiesAssembly => GetType().Namespace + ".Entities";
}