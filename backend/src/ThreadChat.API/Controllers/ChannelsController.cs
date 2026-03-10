using Microsoft.AspNetCore.Mvc;
using ThreadChat.Application.DTOs.Channels;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChannelsController : ControllerBase
{
    private readonly IChannelRepository _channelRepository;
    private readonly ThreadChatDbContext _dbContext;

    public ChannelsController(IChannelRepository channelRepository, ThreadChatDbContext dbContext)
    {
        _channelRepository = channelRepository;
        _dbContext = dbContext;
    }

    [HttpGet("by-workspace/{workspaceId:guid}")]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> GetByWorkspace(Guid workspaceId, CancellationToken cancellationToken)
    {
        var channels = await _channelRepository.ListByWorkspaceIdAsync(workspaceId, cancellationToken);

        var result = channels.Select(c => new ChannelDto(
            c.Id,
            c.WorkspaceId,
            c.Name,
            c.Type,
            c.IsPrivate,
            c.CreatedAt));

        return Ok(result);
    }

    public sealed class CreateChannelRequest
    {
        public Guid WorkspaceId { get; init; }
        public string Name { get; init; } = null!;
        public ChannelType Type { get; init; } = ChannelType.Text;
        public bool IsPrivate { get; init; }
    }

    [HttpPost]
    public async Task<ActionResult<ChannelDto>> Create([FromBody] CreateChannelRequest request, CancellationToken cancellationToken)
    {
        if (request.WorkspaceId == Guid.Empty)
        {
            return BadRequest(new { error = "WorkspaceId is required" });
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { error = "Name is required" });
        }

        var channel = new Channel
        {
            Id = Guid.NewGuid(),
            WorkspaceId = request.WorkspaceId,
            Name = request.Name.Trim(),
            Type = request.Type,
            IsPrivate = request.IsPrivate,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _dbContext.Channels.AddAsync(channel, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var dto = new ChannelDto(
            channel.Id,
            channel.WorkspaceId,
            channel.Name,
            channel.Type,
            channel.IsPrivate,
            channel.CreatedAt);

        return CreatedAtAction(nameof(GetByWorkspace), new { workspaceId = channel.WorkspaceId }, dto);
    }
}

