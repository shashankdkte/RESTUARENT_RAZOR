using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using ABBY.UTILITY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
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

		public IActionResult OnPost()
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
                int quantity = ShoppingCartList.ToList().Count();
               
                _unitOfWork.Save();


				var domain = "https://localhost:7125/";
				var options = new SessionCreateOptions
				{
					LineItems = new List<SessionLineItemOptions>(),
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
					Mode = "payment",
					SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={OrderHeader.Id}",
					CancelUrl = domain + $"customer/cart/index",
				};

                //add line items
                foreach(var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name,
                            },
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
               
                
                var service = new SessionService();
				Session session = service.Create(options);

				Response.Headers.Add("Location", session.Url);
                OrderHeader.SessionId = session.Id;
             //   OrderHeader.SessionPaymentIntentId = session.PaymentIntentId;
                
                _unitOfWork.Save();
                // OrderHeader.TransactionId = session.Id;
                return new StatusCodeResult(303);
			}
            return Page();
		}
	}
}
