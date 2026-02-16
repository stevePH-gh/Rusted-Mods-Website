using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RustedMods.Data;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace RustedMods.Pages
{
    public class SignupModel : PageModel
    {
        private readonly AppDbContext _db;

        public SignupModel(AppDbContext db) => _db = db;

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string? Socials { get; set; }

        [BindProperty]
        public bool ShowVerifyModal { get; set; } = false;


        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                ModelState.AddModelError(nameof(Username), "Field cannot be empty.");
            }
            else if (_db.Users.Any(u => u.Username.ToLower() == Username.ToLower()))
            {
                ModelState.AddModelError(nameof(Username), "Username is already taken.");
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ModelState.AddModelError(nameof(Email), "Field cannot be empty.");
            }
            else if (_db.Users.Any(u => u.Email.ToLower() == Email.ToLower()))
            {
                ModelState.AddModelError(nameof(Email), "Email is already registered.");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError(nameof(Password), "Field cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ModelState.AddModelError(nameof(ConfirmPassword), "Field cannot be empty.");
            }

            if (!string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                Password != ConfirmPassword)
            {
                ModelState.AddModelError(nameof(ConfirmPassword), "Passwords do not match.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string confirmationCode = new Random().Next(0, 10000).ToString("D4");

            var user = new UserAccount
            {
                Email = Email,
                Username = Username,
                Password = Password,
                Socials = Socials ?? "",
                Role = "User",
                Verified = false,
                CodeConfirm = confirmationCode,
                Picture = "/images/user_profiles/icon_user-default.png"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            try
            {
                SendConfirmEmail(Email, confirmationCode);
            }
            catch
            {

            }

            ShowVerifyModal = true;
            return Page();
        }

        public IActionResult OnGetCheckUsername(string u)
        {
            if (string.IsNullOrWhiteSpace(u))
                return new JsonResult(new { exists = false });

            bool exists = _db.Users.Any(user => user.Username.ToLower() == u.ToLower());
            return new JsonResult(new { exists });
        }

        private void SendConfirmEmail(string toEmail, string code)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("rustedmodsofficial@gmail.com", "ahaz scns yobz ktes"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("rustedmodsofficial@gmail.com", "RustedMods"),
                Subject = "RM Confirmation Email",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            string logoUrl = "https://i.imgur.com/Z3PtETe.png";

            mailMessage.Body = $@"
                <div style='font-family: Arial, sans-serif; color:#333;'>
                    <img src='{logoUrl}' alt='RustedMods Logo' style='width:120px; height:auto;' /><br/><br/>
                    <p>Hi,</p>
                    <p>Thank you for signing up! Please use the following confirmation code:</p>
                    <h2 style='color:#25b79b;'>{code}</h2>
                    <p>Enter this code in the app to confirm your email.</p>
                    <p>RustedMods</p>
                </div>
            ";

            smtpClient.Send(mailMessage);
        }

    }
}
