using ABBY.DATAACCESS;
using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
           
          Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
          
        }

        public async Task<IActionResult> OnPost(Category category)
        {

            //ModelState.AddModelError(string.Empty,"")
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == category.Id);
            if(categoryFromDb!= null)
            {
                _unitOfWork.Category.Remove(categoryFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
                return RedirectToPage("Index");
            }
           
            
            return Page();

        }
    }
}
