using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ABBYWEB.Pages.Customer.Home
{
    public class DetailModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public DetailModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MenuItem MenuItem { get; set; }
        [Range(1,100,ErrorMessage ="Must be between 1 and 100")]
        public int Count { get; set; }
        public void OnGet(int id )
        {
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType");
        }
    }
}
