using Microsoft.EntityFrameworkCore;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.Infrastructure.Repositories;

public sealed class CallRepository : EfRepository<Call>, ICallRepository
{
    public CallRepository(ThreadChatDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Call>> ListByChannelIdAsync(Guid channelId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Where(c => c.ChannelId == channelId)
            .OrderByDescending(c => c.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Call>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Where(c => c.HostId == userId)
            .OrderByDescending(c => c.StartTime)
            .ToListAsync(cancellationToken);
    }
}

