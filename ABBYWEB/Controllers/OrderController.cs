using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.UTILITY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABBYWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostEnvironment;
        public OrderController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Get(string? status=null)
        {
            var orderList = _unitOfWork.OrderHeader.GetAll(includeProperties:"ApplicationUser");
            if(status == "cancelled")
            {
                orderList = orderList.Where(u => u.Status == SD.StatusCancelled).ToList();
            }
            else if (status == "completed")
            {
                orderList = orderList.Where(u => u.Status == SD.StatusCompleted).ToList();
            }
            else if (status == "ready")
            {
                orderList = orderList.Where(u => u.Status == SD.StatusReady).ToList();
            }
            else if (status == "inprocess")
            {
                orderList = orderList.Where(u => u.Status == SD.StatusInProcess).ToList();
            }
            return Json(new {data = orderList});
        }

        
    }
}
