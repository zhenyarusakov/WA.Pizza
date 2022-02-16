using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WA.Pizza.Core.Entities.IdentityModels;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Core.Entities;

public class ApplicationUser: IdentityUser
{
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public List<TokenModel> RefreshTokens { get; set; } = new();
}