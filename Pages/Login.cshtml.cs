using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RustedMods.Data;
using System.ComponentModel.DataAnnotations;

namespace RustedMods.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;

        public LoginModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        // Controls modal visibility
        public bool ShowLoginModal { get; set; } = false;

        public void OnGet()
        {
            // When /Login is requested directly, open modal
            ShowLoginModal = true;
        }

        public IActionResult OnPost()
        {
            ShowLoginModal = true;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                ModelState.AddModelError(nameof(Email), "Account does not exist.");
                return Page();
            }

            if (!user.Confirmed)
            {
                ModelState.AddModelError(nameof(Email), "Email not confirmed.");
                return Page();
            }

            if (user.Password != Password)
            {
                ModelState.AddModelError(nameof(Password), "Incorrect password.");
                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Email", user.Email);
            return RedirectToPage("/Index");
        }
    }
}
