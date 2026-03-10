using System.ComponentModel.DataAnnotations;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.DTOs.Auth;

public sealed class RegisterRequest
{
    [Required]
    [MaxLength(100)]
    public string Username { get; init; } = null!;

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; init; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; init; } = null!;

    [Required]
    [MaxLength(200)]
    public string FullName { get; init; } = null!;

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Password { get; init; } = null!;

    public SystemRole SystemRole { get; init; } = SystemRole.StandardUser;
}

