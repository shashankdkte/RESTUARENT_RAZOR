using ABBY.DATAACCESS;
using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Category Category { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(Category category)
        {

            //ModelState.AddModelError(string.Empty,"")
            if(ModelState.IsValid)
            {
                 _unitOfWork.Category.Add(category);
                 _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
            }
            return Page();
            
        }
    }
}
