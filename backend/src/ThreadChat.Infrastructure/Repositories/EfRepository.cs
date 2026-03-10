using Microsoft.EntityFrameworkCore;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.Infrastructure.Repositories;

public abstract class EfRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected EfRepository(ThreadChatDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    protected ThreadChatDbContext DbContext { get; }
    protected DbSet<TEntity> DbSet { get; }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync([id], cancellationToken);

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().ToListAsync(cancellationToken);

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await DbSet.AddAsync(entity, cancellationToken);

    public virtual void Update(TEntity entity) => DbSet.Update(entity);

    public virtual void Remove(TEntity entity) => DbSet.Remove(entity);
}

