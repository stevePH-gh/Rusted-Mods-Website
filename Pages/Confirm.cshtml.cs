using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RustedMods.Data;
using System.ComponentModel.DataAnnotations;

namespace RustedMods.Pages
{
    public class ConfirmModel : PageModel
    {
        private readonly AppDbContext _db;

        public ConfirmModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Code must be 4 digits.")]

        public string Code { get; set; }

        public string? ResultMessage { get; set; }
        public bool IsSuccess { get; set; } = false;

        [BindProperty]
        public bool ShowConfirmation { get; set; } = false;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = _db.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                ResultMessage = "Email not found.";
                return Page();
            }

            if (user.CodeConfirm != Code)
            {
                ResultMessage = "Confirmation code does not match.";
                return Page();
            }

            user.Confirmed = true;
            // user.CodeConfirm = null; // optional but recommended
            _db.SaveChanges();

            IsSuccess = true;
            ResultMessage = "Code matched successfully.";

            ShowConfirmation = true;
            return Page();
        }
    }
}
