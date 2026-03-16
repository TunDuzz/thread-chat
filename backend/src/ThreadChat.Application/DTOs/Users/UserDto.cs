using System;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.DTOs.Users;

public sealed record UserDto(
    Guid Id,
    string Username,
    string PhoneNumber,
    string Email,
    string FirstName,
    string LastName,
    string? Gender,
    DateTimeOffset? DateOfBirth,
    SystemRole SystemRole,
    DateTimeOffset CreatedAt);

