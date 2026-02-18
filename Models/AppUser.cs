using Microsoft.AspNetCore.Identity;

namespace MESSAGENOID.Models
{
    public class AppUser : IdentityUser
    {
        public string user_name { set; get; }

        public string? bio { set; get; }

        public string? profile_pic_url { set; get; }

        public string publicKey { set; get; }
    }
}
