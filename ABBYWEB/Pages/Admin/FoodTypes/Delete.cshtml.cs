using ABBY.DATAACCESS;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public FoodType FoodType { get; set; }
        public void OnGet(int id)
        {

            FoodType = _db.FoodType.FirstOrDefault(u => u.Id == id);
          
        }

        public async Task<IActionResult> OnPost(FoodType foodType)
        {

            //ModelState.AddModelError(string.Empty,"")
            var foodTypeFromDb = _db.FoodType.FirstOrDefault(u => u.Id == foodType.Id);
            if(foodTypeFromDb != null)
            {
                _db.Remove(foodTypeFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "FoodType deleted successfully";
                return RedirectToPage("Index");
            }
           
            
            return Page();

        }
    }
}
