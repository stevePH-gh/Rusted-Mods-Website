using System.ComponentModel.DataAnnotations;

namespace RustedMods.Data
{
    public class UserAccount
    {
        [Key] // <-- this marks it as primary key
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Socials { get; set; }

        public string Role { get; set; }
        public bool Verified { get; set; }
        public bool Confirmed { get; set; } = false;
        public string Picture { get; set; }
        public string CodeConfirm { get; set; }
    }

}
