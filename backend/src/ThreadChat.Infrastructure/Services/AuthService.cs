using Microsoft.EntityFrameworkCore;
using ThreadChat.Application.Abstractions;
using ThreadChat.Application.DTOs.Auth;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Application.Interfaces.Services;
using ThreadChat.Domain.Entities;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.Infrastructure.Services;

public sealed class AuthService : IAuthService
{
    private readonly ThreadChatDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        ThreadChatDbContext dbContext,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (await _userRepository.ExistsByUsernameAsync(request.Username, cancellationToken))
        {
            return Result<AuthResponse>.Failure("Username already exists.");
        }

        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            return Result<AuthResponse>.Failure("Email already exists.");
        }

        if (await _userRepository.ExistsByPhoneNumberAsync(request.PhoneNumber, cancellationToken))
        {
            return Result<AuthResponse>.Failure("Phone number already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            FullName = request.FullName,
            PasswordHash = _passwordHasher.Hash(request.Password),
            SystemRole = request.SystemRole,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var accessToken = _jwtTokenService.CreateAccessToken(user);
        var refreshToken = _jwtTokenService.CreateRefreshToken();
        var expiresAt = _jwtTokenService.GetAccessTokenExpiresAtUtc();

        var response = new AuthResponse(
            user.Id,
            user.Username,
            user.Email,
            accessToken,
            expiresAt,
            refreshToken);

        return Result<AuthResponse>.Success(response);
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var usernameOrEmail = request.UsernameOrEmail.Trim();

        User? user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.Username == usernameOrEmail || u.Email == usernameOrEmail,
                cancellationToken);

        if (user is null)
        {
            return Result<AuthResponse>.Failure("Invalid credentials.");
        }

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Result<AuthResponse>.Failure("Invalid credentials.");
        }

        var accessToken = _jwtTokenService.CreateAccessToken(user);
        var refreshToken = _jwtTokenService.CreateRefreshToken();
        var expiresAt = _jwtTokenService.GetAccessTokenExpiresAtUtc();

        var response = new AuthResponse(
            user.Id,
            user.Username,
            user.Email,
            accessToken,
            expiresAt,
            refreshToken);

        return Result<AuthResponse>.Success(response);
    }
}

