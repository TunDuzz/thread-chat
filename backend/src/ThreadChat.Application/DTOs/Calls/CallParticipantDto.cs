using System;

namespace ThreadChat.Application.DTOs.Calls;

public sealed record CallParticipantDto(
    Guid Id,
    Guid CallId,
    Guid UserId,
    DateTimeOffset JoinedAt,
    DateTimeOffset? LeftAt);

