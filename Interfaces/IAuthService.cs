using MESSAGENOID.Models;
using Microsoft.AspNetCore.Identity;

namespace MESSAGENOID.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(string email, string password, string userName,string publicKey);
        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        
    }
}
