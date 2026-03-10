using Microsoft.AspNetCore.Mvc;
using ThreadChat.Application.Abstractions;
using ThreadChat.Application.DTOs.Messages;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;
    private readonly ThreadChatDbContext _dbContext;

    public MessagesController(IMessageRepository messageRepository, ThreadChatDbContext dbContext)
    {
        _messageRepository = messageRepository;
        _dbContext = dbContext;
    }

    [HttpGet("by-channel/{channelId:guid}")]
    public async Task<ActionResult<PagedResult<MessageDto>>> GetByChannel(
        Guid channelId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var request = new PagedRequest(page, pageSize);

        var pagedMessages = await _messageRepository.GetChannelMessagesAsync(channelId, request, cancellationToken);

        var dtoItems = pagedMessages.Items
            .Select(m => new MessageDto(
                m.Id,
                m.ChannelId,
                m.SenderId,
                m.MessageType,
                m.Content,
                m.FileUrl,
                m.ReplyToId,
                m.SentAt))
            .ToList();

        var result = new PagedResult<MessageDto>(
            dtoItems,
            pagedMessages.TotalCount,
            pagedMessages.Page,
            pagedMessages.PageSize);

        return Ok(result);
    }

    public sealed class CreateMessageRequest
    {
        public Guid ChannelId { get; init; }
        public Guid SenderId { get; init; }
        public MessageType MessageType { get; init; } = MessageType.Text;
        public string? Content { get; init; }
        public string? FileUrl { get; init; }
        public Guid? ReplyToId { get; init; }
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> Create([FromBody] CreateMessageRequest request, CancellationToken cancellationToken)
    {
        if (request.ChannelId == Guid.Empty)
        {
            return BadRequest(new { error = "ChannelId is required" });
        }

        if (request.SenderId == Guid.Empty)
        {
            return BadRequest(new { error = "SenderId is required" });
        }

        if (request.MessageType == MessageType.Text && string.IsNullOrWhiteSpace(request.Content))
        {
            return BadRequest(new { error = "Content is required for text messages" });
        }

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ChannelId = request.ChannelId,
            SenderId = request.SenderId,
            MessageType = request.MessageType,
            Content = request.Content,
            FileUrl = request.FileUrl,
            ReplyToId = request.ReplyToId,
            SentAt = DateTimeOffset.UtcNow
        };

        await _dbContext.Messages.AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var dto = new MessageDto(
            message.Id,
            message.ChannelId,
            message.SenderId,
            message.MessageType,
            message.Content,
            message.FileUrl,
            message.ReplyToId,
            message.SentAt);

        return CreatedAtAction(nameof(GetByChannel), new { channelId = message.ChannelId }, dto);
    }
}

