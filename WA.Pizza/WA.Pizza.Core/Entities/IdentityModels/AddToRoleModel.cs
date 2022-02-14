using System.ComponentModel.DataAnnotations;

namespace WA.Pizza.Core.Entities.IdentityModels;

public class AddToRoleModel
{
    public AddToRoleModel(string email, string role)
    {
        Email = email;
        Role = role;
    }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }
}