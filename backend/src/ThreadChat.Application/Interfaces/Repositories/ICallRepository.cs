using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.Interfaces.Repositories;

public interface ICallRepository : IRepository<Call>
{
    Task<IReadOnlyList<Call>> ListByChannelIdAsync(Guid channelId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Call>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}

