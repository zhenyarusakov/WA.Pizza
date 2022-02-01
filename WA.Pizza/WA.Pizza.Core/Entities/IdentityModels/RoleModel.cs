using System.ComponentModel.DataAnnotations;

namespace WA.Pizza.Core.Entities.IdentityModels;

public class RoleModel
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }
}