using System.Threading.Tasks;
using WA.Pizza.Core.Entities.IdentityModels;

namespace WA.Pizza.Infrastructure.Abstractions;

public interface IAuthenticateService
{
    Task<Response> Register(RegisterModel model);
    Task<Response> RegisterAdmin(RegisterModel model);
    Task<TokenModel> Login(LoginModel model);

}