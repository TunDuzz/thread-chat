using Microsoft.AspNetCore.Mvc;
using ThreadChat.Application.DTOs.Workspaces;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkspacesController : ControllerBase
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly ThreadChatDbContext _dbContext;

    public WorkspacesController(IWorkspaceRepository workspaceRepository, ThreadChatDbContext dbContext)
    {
        _workspaceRepository = workspaceRepository;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceDto>>> GetAll(CancellationToken cancellationToken)
    {
        var workspaces = await _workspaceRepository.ListAsync(cancellationToken);

        var result = workspaces.Select(w => new WorkspaceDto(
            w.Id,
            w.Name,
            w.AvatarUrl,
            w.CreatedAt));

        return Ok(result);
    }

    public sealed class CreateWorkspaceRequest
    {
        public string Name { get; init; } = null!;
        public string? AvatarUrl { get; init; }
    }

    [HttpPost]
    public async Task<ActionResult<WorkspaceDto>> Create([FromBody] CreateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { error = "Name is required" });
        }

        var workspace = new Workspace
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            AvatarUrl = request.AvatarUrl,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _dbContext.Workspaces.AddAsync(workspace, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var dto = new WorkspaceDto(
            workspace.Id,
            workspace.Name,
            workspace.AvatarUrl,
            workspace.CreatedAt);

        return CreatedAtAction(nameof(GetAll), new { id = workspace.Id }, dto);
    }
}

