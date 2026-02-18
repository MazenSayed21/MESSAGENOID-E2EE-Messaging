using MESSAGENOID.Models;

namespace MESSAGENOID.Interfaces
{
    public interface IMessageService
    {
        Task<bool> SendMessageAsync(string receiverId, string content);

        Task<IEnumerable<Message>> GetMessagesForUserAsync(string userId);
        Task<Message?> GetMessageByIdAsync(int messageId);
        Task<bool> DeleteMessageAsync(int messageId, string userId); // Ensure only owner can delete

        Task<int> GetTotalMessageCountAsync();
    }
}
