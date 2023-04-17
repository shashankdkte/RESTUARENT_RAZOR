using ABBYWEB.Data;
using ABBYWEB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
           
                Category = _db.Category.FirstOrDefault(u => u.Id == id);
          
        }

        public async Task<IActionResult> OnPost(Category category)
        {

            //ModelState.AddModelError(string.Empty,"")
            if (ModelState.IsValid)
            {
                 _db.Update(category);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
