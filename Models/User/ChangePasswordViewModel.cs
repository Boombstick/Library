namespace Library.Models.User
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
