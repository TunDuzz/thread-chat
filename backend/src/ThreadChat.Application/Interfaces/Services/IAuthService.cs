using System.Threading;
using System.Threading.Tasks;
using ThreadChat.Application.Abstractions;
using ThreadChat.Application.DTOs.Auth;

namespace ThreadChat.Application.Interfaces.Services;

public interface IAuthService
{
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}

