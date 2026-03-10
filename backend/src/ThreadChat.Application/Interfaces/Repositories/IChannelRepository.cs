using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.Interfaces.Repositories;

public interface IChannelRepository : IRepository<Channel>
{
    Task<IReadOnlyList<Channel>> ListByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Channel>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}

