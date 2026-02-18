namespace MESSAGENOID.Models.ViewModels
{
    public class profileVM
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string Email { get; set; }
        public string? ProfilePicUrl { get; set; }
        public bool IsOwnProfile { get; set; }
    }
}
