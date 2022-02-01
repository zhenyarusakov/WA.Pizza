using System;

namespace WA.Pizza.Core.Entities.IdentityModels;

public class TokenModel
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string Message { get; set; }
}