using MESSAGENOID.Interfaces;
using MESSAGENOID.Models;
using MESSAGENOID.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MESSAGENOID.Services
{
    public class SearchService : ISearchService
    {
        private readonly UserManager<AppUser> _userManager;

        public SearchService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserSearchResultVM>> SearchUsersAsync(string query, int limit = 20)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<UserSearchResultVM>();

            query = query.Trim();

            return await _userManager.Users
                .AsNoTracking()
                .Where(u => u.user_name.StartsWith(query) || u.user_name.Contains(query) || u.Email.Contains(query))
                .OrderByDescending(u => u.user_name.StartsWith(query))
                .ThenBy(u => u.user_name)
                .Take(limit)
                .Select(u => new UserSearchResultVM
                {
                    id = u.Id,
                    DisplayName = u.user_name,
                    userName = u.UserName,
                    PublicKey = u.publicKey
                })
                .ToListAsync();
        }
    }
}
