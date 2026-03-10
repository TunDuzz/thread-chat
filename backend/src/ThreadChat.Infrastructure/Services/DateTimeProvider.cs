using ThreadChat.Application.Interfaces.Services;

namespace ThreadChat.Infrastructure.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}

