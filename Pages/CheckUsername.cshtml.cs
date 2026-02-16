using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RustedMods.Data;
using System.Linq;

namespace RustedMods.Pages
{
    public class CheckUsernameModel : PageModel
    {
        private readonly AppDbContext _db;

        public CheckUsernameModel(AppDbContext db) => _db = db;

        public JsonResult OnGet(string u)
        {
            bool exists = _db.Users.Any(x => x.Username == u.ToLower().Trim());
            return new JsonResult(new { exists });
        }
    }
}
