using System;

namespace WA.Pizza.Core.Entities.IdentityModels;

public class AuthenticationResponse
{
    public AuthenticationResponse(string accessToken, string? refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; private set; }
    
    public string? RefreshToken { get; private set; }

    public DateTime RefreshTokenExpiration { get; init; }
}