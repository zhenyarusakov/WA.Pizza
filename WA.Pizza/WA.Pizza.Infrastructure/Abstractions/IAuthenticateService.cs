using System.Threading.Tasks;
using WA.Pizza.Core.Entities.IdentityModels;

namespace WA.Pizza.Infrastructure.Abstractions;

public interface IAuthenticateService
{
    Task<Response> RegisterAsync(RegisterModel model);
    Task<Response> RegisterAdminAsync(RegisterModel model);
    Task<TokenModel> LoginAsync(LoginModel model);
    Task<string> AddRoleAsync(RoleModel roleModel);

}