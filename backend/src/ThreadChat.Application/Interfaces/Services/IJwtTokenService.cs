using System;
using System.Collections.Generic;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string CreateAccessToken(User user, IReadOnlyDictionary<string, string>? extraClaims = null);
    string CreateRefreshToken();

    DateTimeOffset GetAccessTokenExpiresAtUtc();
}

