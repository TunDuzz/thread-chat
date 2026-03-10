using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThreadChat.Application.Interfaces.Repositories;
using ThreadChat.Application.Interfaces.Services;
using ThreadChat.Infrastructure.Data;
using ThreadChat.Infrastructure.Repositories;
using ThreadChat.Infrastructure.Services;

namespace ThreadChat.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ThreadChatDatabase")
                               ?? throw new InvalidOperationException("Connection string 'ThreadChatDatabase' is not configured.");

        services.AddDbContext<ThreadChatDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.AddScoped<IChannelRepository, ChannelRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<ICallRepository, CallRepository>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}

