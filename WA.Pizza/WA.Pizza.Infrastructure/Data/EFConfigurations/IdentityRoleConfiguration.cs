using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WA.Pizza.Infrastructure.Data.EFConfigurations;

public class IdentityRoleConfiguration: IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = Core.Entities.IdentityModels.UserRoles.Administrator,
                NormalizedName = Core.Entities.IdentityModels.UserRoles.Administrator.ToUpper()
            },
            new IdentityRole
            {
                Name = Core.Entities.IdentityModels.UserRoles.Moderator,
                NormalizedName = Core.Entities.IdentityModels.UserRoles.Moderator.ToUpper()
            },
            new IdentityRole
            {
                Name = Core.Entities.IdentityModels.UserRoles.User,
                NormalizedName = Core.Entities.IdentityModels.UserRoles.User.ToUpper()
            }
        );
    }
}