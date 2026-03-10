using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.Interfaces.Repositories;

public interface IWorkspaceRepository : IRepository<Workspace>
{
    Task<IReadOnlyList<Workspace>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}

