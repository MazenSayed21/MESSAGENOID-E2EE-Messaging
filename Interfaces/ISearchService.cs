using MESSAGENOID.Models.ViewModels;
namespace MESSAGENOID.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<UserSearchResultVM>> SearchUsersAsync(string query, int limit = 20);
    }
}
