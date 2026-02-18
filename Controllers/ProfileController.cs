using MESSAGENOID.Models;
using MESSAGENOID.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MESSAGENOID.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
        }

        [Authorize]
        public async Task<IActionResult> Index(string id)
        {
            var userId = id ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var vm = new profileVM {
                Id = user.Id,
                DisplayName = user.user_name,
                Bio = user.bio,
                Email = user.Email,
                ProfilePicUrl = user.profile_pic_url,
                IsOwnProfile = (id==null)
            };

            if (user == null) return NotFound();

            return View(vm);
        }

        // GET: /Profile/Edit
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null) return NotFound();

                var model = new EditProfileVM
                {
                    displayName = user.user_name,
                    bio = user.bio,
                    profilePic = user.profile_pic_url
                };

                return View(model);
            
        }

        // POST: /Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            // Update basic info
            if(model.bio!=null)user.bio = model.bio;
            if(model.profilePic!=null)user.profile_pic_url = model.profilePic;
            if (model.displayName != null) user.user_name = model.displayName;
            // Handle Profile Picture upload if a new one is provided

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index","Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

      
    }
}
