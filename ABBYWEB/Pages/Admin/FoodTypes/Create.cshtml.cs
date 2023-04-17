using ABBY.DATAACCESS;
using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                _unitOfWork.FoodType.Add(foodType);
                _unitOfWork.Save();
                TempData["success"] = "FoodType created successfully";
                return RedirectToPage("Index");
            }
            return Page();
            
        }
    }
}
