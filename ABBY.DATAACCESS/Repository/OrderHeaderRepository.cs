﻿using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBY.DATAACCESS.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRespository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
           _db = db;
        }

       
        public void Update(OrderHeader orderHeader)
        {
            _db.Update(orderHeader);
        }

        public void UpdateStatus(int id, string status)
        {
            var dbOject = _db.OrderHeader.FirstOrDefault(o => o.Id == id);
            if (dbOject != null)
            {
                dbOject.Status = status;
            }
        }
    }
}
