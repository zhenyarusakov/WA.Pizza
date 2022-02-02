using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Entities.IdentityModels;

namespace WA.Pizza.Infrastructure.Data.EFConfigurations;

public class IdentityUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var admin = new ApplicationUser
        {
            UserName = BaseAuthorization.default_admin,
            Email = BaseAuthorization.default_email,
            PasswordHash = BaseAuthorization.default_password,
            NormalizedUserName = BaseAuthorization.default_admin.ToUpper(),
            NormalizedEmail = BaseAuthorization.default_email.ToUpper(),
            EmailConfirmed = true
        };

        admin.PasswordHash = PassGenerate(admin);

        builder.HasData(admin);
    }
    
    private string PassGenerate(ApplicationUser user)
    {
        var passHash = new PasswordHasher<ApplicationUser>();
        return passHash.HashPassword(user, BaseAuthorization.default_password);
    }
}