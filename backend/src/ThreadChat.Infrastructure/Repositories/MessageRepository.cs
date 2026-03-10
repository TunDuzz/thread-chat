using Microsoft.EntityFrameworkCore;
using ThreadChat.Application.Abstractions;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.Infrastructure.Repositories;

public sealed class MessageRepository : EfRepository<Message>, IMessageRepository
{
    public MessageRepository(ThreadChatDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedResult<Message>> GetChannelMessagesAsync(
        Guid channelId,
        PagedRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsNoTracking()
            .Where(m => m.ChannelId == channelId)
            .OrderByDescending(m => m.SentAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Message>(items, totalCount, request.Page, request.PageSize);
    }
}

