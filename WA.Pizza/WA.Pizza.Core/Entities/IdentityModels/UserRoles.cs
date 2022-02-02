using System.Collections.Generic;

namespace WA.Pizza.Core.Entities.IdentityModels;

public static class UserRoles
{
    public const string Administrator = "Administrator";

    public const string Moderator ="Moderator";

    public const string User = "User";

    public static IReadOnlyCollection<string> ListofRoles = new []{ Administrator, Moderator, User };
}