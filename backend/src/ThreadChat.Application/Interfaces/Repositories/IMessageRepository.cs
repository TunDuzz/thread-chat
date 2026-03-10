using System;
using System.Threading;
using System.Threading.Tasks;
using ThreadChat.Application.Abstractions;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.Interfaces.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    Task<PagedResult<Message>> GetChannelMessagesAsync(
        Guid channelId,
        PagedRequest request,
        CancellationToken cancellationToken = default);
}

