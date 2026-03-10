using System;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.DTOs.Users;

public sealed record UserDto(
    Guid Id,
    string Username,
    string PhoneNumber,
    string Email,
    string FullName,
    SystemRole SystemRole,
    DateTimeOffset CreatedAt);

