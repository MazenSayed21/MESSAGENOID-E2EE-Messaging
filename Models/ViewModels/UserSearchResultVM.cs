namespace MESSAGENOID.Models.ViewModels
{
    public class UserSearchResultVM
    {
        public string userName { set; get; }
        public string id { get; set; }

        public string DisplayName { get; set; }

        public string ProfilePicUrl { get; set; }

        public string Bio { get; set; }

        public string PublicKey { get; set; }
    }
}
