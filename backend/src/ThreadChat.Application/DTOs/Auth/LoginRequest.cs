using System.ComponentModel.DataAnnotations;

namespace ThreadChat.Application.DTOs.Auth;

public sealed class LoginRequest
{
    [Required]
    public string UsernameOrEmail { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;
}

