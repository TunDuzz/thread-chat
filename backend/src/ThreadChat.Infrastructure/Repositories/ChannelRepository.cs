using Microsoft.EntityFrameworkCore;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.Infrastructure.Repositories;

public sealed class ChannelRepository : EfRepository<Channel>, IChannelRepository
{
    public ChannelRepository(ThreadChatDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Channel>> ListByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Where(c => c.WorkspaceId == workspaceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Channel>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.ChannelMembers
            .AsNoTracking()
            .Where(cm => cm.UserId == userId)
            .Select(cm => cm.Channel)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}

