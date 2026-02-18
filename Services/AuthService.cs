using MESSAGENOID.Interfaces;
using MESSAGENOID.Models;
using Microsoft.AspNetCore.Identity;

namespace MESSAGENOID.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // 1. Register a new user with their Public Key for E2EE
        public async Task<IdentityResult> RegisterAsync(string email, string password, string userName, string publicKey)
        {
            var user = new AppUser
            {
                Email = email,
                UserName = email, // Identity uses UserName for login (usually the email)
                user_name = userName, // Your custom display name property
                publicKey = publicKey, // Storing the RSA Public Key
            };

            var result = await _userManager.CreateAsync(user, password);

            // Note: In a real app, you might want to assign a "User" role here
            // await _userManager.AddToRoleAsync(user, "User");

            return result;
        }

        // 2. Login using Email and Password
        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            // We find the user by email first because Identity's default Login uses UserName
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            // lockoutOnFailure: true is better for security in portfolio projects!
            return await _signInManager.PasswordSignInAsync(user.UserName, password, rememberMe, lockoutOnFailure: true);
        }

        // 3. Clear the authentication cookie
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
