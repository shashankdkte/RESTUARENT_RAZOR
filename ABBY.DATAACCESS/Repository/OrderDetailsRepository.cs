using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBY.DATAACCESS.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails> , IOrderDetailsRespository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
           _db = db;
        }

       
        public void Update(OrderDetails orderDetails)
        {
            _db.Update(orderDetails);
        }
    }
}
