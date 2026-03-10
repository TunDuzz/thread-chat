using System;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.DTOs.Messages;

public sealed record MessageDto(
    Guid Id,
    Guid ChannelId,
    Guid SenderId,
    MessageType MessageType,
    string? Content,
    string? FileUrl,
    Guid? ReplyToId,
    DateTimeOffset SentAt);

