using ABBY.DATAACCESS;
using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public FoodType FoodType { get; set; }
        public void OnGet(int id)
        {

            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
          
        }

        public async Task<IActionResult> OnPost(FoodType foodType)
        {

            //ModelState.AddModelError(string.Empty,"")
            if (ModelState.IsValid)
            {
                _unitOfWork.FoodType.Update(foodType);
                _unitOfWork.Save();
                TempData["success"] = "FoodType updated successfully";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
