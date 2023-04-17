using ABBY.DATAACCESS;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public FoodType FoodType { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(FoodType foodType)
        {

            //ModelState.AddModelError(string.Empty,"")
            if(ModelState.IsValid)
            {
                await _db.AddAsync(foodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "FoodType created successfully";
                return RedirectToPage("Index");
            }
            return Page();
            
        }
    }
}
