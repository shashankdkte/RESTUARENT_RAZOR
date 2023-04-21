using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using ABBY.MODELS.ViewModel;
using ABBY.UTILITY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABBYWEB.Pages.Admin.Order
{
    [Authorize(Roles = $"{SD.ManagerRole},{SD.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<OrderDetailsVM> orderDetailsVM { get; set; }

        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public void OnGet()
        {

            orderDetailsVM = new();

            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == SD.StatusSubmitted || u.Status == SD.StatusInProcess).ToList();
            foreach(var  orderHeader in orderHeaders)
            {
                OrderDetailsVM item = new()
                {
                    
                    OrderHeader = orderHeader,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == orderHeader.Id).ToList(),
                };
                orderDetailsVM.Add(item);
            }
        }

        public IActionResult OnPostOrderInProcess(int orderId) {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
