using MESSAGENOID.Data;
using MESSAGENOID.Interfaces;
using MESSAGENOID.Models;
using Microsoft.EntityFrameworkCore;

namespace MESSAGENOID.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendMessageAsync(string receiverId, string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return false;

            var newMessage = new Message
            {
                content = content, // This is the encrypted ciphertext from JS
                reciever_id = receiverId,
                time_stamp = DateTime.UtcNow // Professional standard: always use UTC
            };

            await _context.messages.AddAsync(newMessage);
            return await _context.SaveChangesAsync() > 0;
        }

        // 2. Get all messages for a specific user's inbox
        public async Task<IEnumerable<Message>> GetMessagesForUserAsync(string userId)
        {
            return await _context.messages
                .Where(m => m.reciever_id == userId)
                .OrderByDescending(m => m.time_stamp)
                .ToListAsync();
        }

        // 3. Get a single message by ID
        public async Task<Message?> GetMessageByIdAsync(int messageId)
        {
            return await _context.messages
                .Include(m => m.Reciever) // Optional: include user details if needed
                .FirstOrDefaultAsync(m => m.message_id == messageId);
        }

        // 4. Delete Message with Security Check
        public async Task<bool> DeleteMessageAsync(int messageId, string userId)
        {
            var message = await _context.messages.FindAsync(messageId);

            // SECURITY: Ensure message exists AND belongs to the requesting user
            if (message == null || message.reciever_id != userId)
            {
                return false;
            }

            _context.messages.Remove(message);
            return await _context.SaveChangesAsync() > 0;
        }

        // 5. System Stats (Great for Admin Dashboards)
        public async Task<int> GetTotalMessageCountAsync()
        {
            return 0;
        }

    }
}
