using ABBY.DATAACCESS;
using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
           
                Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
          
        }

        public async Task<IActionResult> OnPost(Category category)
        {

            //ModelState.AddModelError(string.Empty,"")
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
