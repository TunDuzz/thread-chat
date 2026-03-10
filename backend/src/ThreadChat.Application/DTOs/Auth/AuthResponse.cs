namespace ThreadChat.Application.DTOs.Auth;

public sealed record AuthResponse(
    Guid UserId,
    string Username,
    string Email,
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAtUtc,
    string RefreshToken);

