using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
namespace ABBYWEB.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        private IWebHostEnvironment _hostEnvironment;
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironement)
        {
            _unitOfWork = unitOfWork;
            MenuItem = new();
            _hostEnvironment = hostEnvironement;
        }
        public MenuItem MenuItem { get; set; }
        public void OnGet(int? id)
        {
            if(id!=null)
            {
                 MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            }
            CategoryList = _unitOfWork.Category.GetAll().Select(x =>  new SelectListItem()
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost(MenuItem menuItem)
        {
            if(menuItem.Name == null)
            {
                return Page();
            }
            string webRootPath = _hostEnvironment.WebRootPath;
            var files =  HttpContext.Request.Form.Files;
            if(menuItem.Id ==0)
            {
                string newFileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images/menuItems");
                var extension = Path.GetExtension(files[0].FileName);

                //FileStream fileStream? = null;
                using(var fileStream = new FileStream(Path.Combine(uploads,newFileName+extension),FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                menuItem.Image = @"\images\menuItems\" + newFileName + extension;
                //ModelState.AddModelError(string.Empty,"")
                if (ModelState.IsValid)
                { }

                _unitOfWork.MenuItem.Add(menuItem);
                _unitOfWork.Save();
                TempData["success"] = "MENU ITEM CREATED SUCCESSFULLY";
                return RedirectToPage("Index");
            }
            else 
            {
                var dbObject = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == menuItem.Id);
                if(files.Count > 0)
                {
                    string newFileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images/menuItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    //delete the old image

                    var oldImagePath = Path.Combine(webRootPath, dbObject.Image.TrimStart('\\'));
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    //FileStream fileStream? = null;
                    using (var fileStream = new FileStream(Path.Combine(uploads, newFileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    menuItem.Image = @"\images\menuItems\" + newFileName + extension;
                }
                else
                {
                    menuItem.Image = dbObject.Image;

                }
                _unitOfWork.MenuItem.Update(menuItem);
                _unitOfWork.Save();
                return RedirectToPage("Index");
            }

            
        
           

        }
    }
}
