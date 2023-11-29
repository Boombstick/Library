using System.ComponentModel.DataAnnotations;

namespace Library.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRemember { get; set; }

        public string? ReturnUrl { get; set; }   
    }
}

