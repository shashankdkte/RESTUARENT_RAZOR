using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using ABBY.UTILITY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ABBYWEB.Pages.Customer.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        [BindProperty]
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        [BindProperty]
        public OrderHeader OrderHeader { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    filter: u => u.ApplicationUserId == claims.Value, includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
                foreach (var cart in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
                }
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claims.Value);
                OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            }
        }

		public void OnPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claims != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
					filter: u => u.ApplicationUserId == claims.Value, includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
				}
                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderDate = DateTime.Now;
                OrderHeader.UserId = claims.Value;
                OrderHeader.PickupTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " + OrderHeader.PickupTime.ToShortTimeString());

                _unitOfWork.OrderHeader.Add(OrderHeader);
                if (ModelState.IsValid) { }
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new OrderDetails() { 

                        MenuItemId=item.MenuItemId,
                        OrderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);
                  //  _unitOfWork.Save();

				}

                _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
                _unitOfWork.Save();
                 
			}
		}
	}
}
