using System;

namespace WA.Pizza.Core.Entities.IdentityModels;

public static class ConstantValues
{
    public static readonly TimeSpan AccessTokenLifetime;
    public static readonly TimeSpan RefreshTokenLifetime;

    public const string AdminRole = "Admin";

    static ConstantValues()
    {
        AccessTokenLifetime = TimeSpan.FromMinutes(1);
        RefreshTokenLifetime = TimeSpan.FromMinutes(5);
    }
}