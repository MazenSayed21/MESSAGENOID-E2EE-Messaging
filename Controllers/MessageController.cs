using MESSAGENOID.Data;
using MESSAGENOID.Interfaces;
using MESSAGENOID.Models;
using MESSAGENOID.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MESSAGENOID.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(ApplicationDbContext context, IMessageService messageService, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Chat(string id) // 'id' comes from the asp-route-id.
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Inbox");

            // 1. Find the person the user wants to talk to
            var receiver = await _userManager.FindByIdAsync(id);
            if (receiver == null) return NotFound();

            // 2. Prepare the ViewModel with the data for the View
            var vm = new ChatVM
            {
                reciever_id = receiver.Id,
                ReceiverName = receiver.user_name ?? receiver.UserName,
                ReceiverPublicKey = receiver.publicKey // CRITICAL for E2EE
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Chat(string reciever_id, string content)
        {
            if (string.IsNullOrEmpty(reciever_id) || string.IsNullOrEmpty(content))
            {
                TempData["Error"] = "Message cannot be empty.";
                return RedirectToAction("Chat", new { id = reciever_id });
            }

            var success = await _messageService.SendMessageAsync(reciever_id, content);

            if (success)
            {
                TempData["SuccessMessage"] = "Message sent successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to send message!";
            }

            return RedirectToAction("Chat", new { id = reciever_id });
        }

        [HttpGet]
        public async Task<IActionResult> inbox()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var messages = await _messageService.GetMessagesForUserAsync(userId);

            return View(messages);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
