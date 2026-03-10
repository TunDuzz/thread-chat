using System;

namespace ThreadChat.Application.DTOs.Workspaces;

public sealed record WorkspaceDto(
    Guid Id,
    string Name,
    string? AvatarUrl,
    DateTimeOffset CreatedAt);

