using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace RustedMods.Pages
{
    public class FeedbackModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Please select a type.")]
        public string Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Title is required.")]
        public string Subject { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Message is required.")]
        public string Content { get; set; }

        public string? ResultMessage { get; set; }
        public bool IsSuccess { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                SendFeedbackEmail();
                IsSuccess = true;
                ResultMessage = "Feedback sent successfully. Thank you!";
            }
            catch
            {
                IsSuccess = false;
                ResultMessage = "Failed to send feedback. Please try again later.";
            }

            return Page();
        }

        private void SendFeedbackEmail()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    "rustedmodsofficial@gmail.com",
                    "ahaz scns yobz ktes"
                ),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress("rustedmodsofficial@gmail.com", "RustedMods Feedback"),

                Subject = $"[{Type}] {Subject}",

                Body = $@"
<div style='font-family: Arial, sans-serif; color:#333;'>
    <h5><b>{Subject}</b></h5>
    <hr />
    <p>{Content}</p>
    <br />
</div>
",
                IsBodyHtml = true
            };


            mail.To.Add("rustedmodsofficial@gmail.com");

            smtpClient.Send(mail);
        }
    }
}
