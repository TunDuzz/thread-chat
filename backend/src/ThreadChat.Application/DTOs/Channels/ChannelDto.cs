using System;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.DTOs.Channels;

public sealed record ChannelDto(
    Guid Id,
    Guid WorkspaceId,
    string Name,
    ChannelType Type,
    bool IsPrivate,
    DateTimeOffset CreatedAt);

