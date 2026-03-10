using System;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.DTOs.Calls;

public sealed record CallDto(
    Guid Id,
    Guid ChannelId,
    Guid HostId,
    CallType CallType,
    CallStatus Status,
    DateTimeOffset StartTime,
    DateTimeOffset? EndTime);

