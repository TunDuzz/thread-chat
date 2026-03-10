using Microsoft.EntityFrameworkCore;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.Infrastructure.Repositories;

public sealed class WorkspaceRepository : EfRepository<Workspace>, IWorkspaceRepository
{
    public WorkspaceRepository(ThreadChatDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Workspace>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.WorkspaceMembers
            .AsNoTracking()
            .Where(wm => wm.UserId == userId)
            .Select(wm => wm.Workspace)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}

