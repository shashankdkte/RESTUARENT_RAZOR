using ABBY.DATAACCESS;
using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
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
            var foodTypeFromDb = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == foodType.Id);
            if(foodTypeFromDb != null)
            {
                _unitOfWork.FoodType.Remove(foodTypeFromDb);
                _unitOfWork.Save();
                TempData["success"] = "FoodType deleted successfully";
                return RedirectToPage("Index");
            }
           
            
            return Page();

        }
    }
}
