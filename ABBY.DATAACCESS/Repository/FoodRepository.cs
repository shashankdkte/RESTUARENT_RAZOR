using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBY.DATAACCESS.Repository
{
    public class FoodRepository : Repository<FoodType> , IFoodRepository
    {
        private readonly ApplicationDbContext _db;
        public FoodRepository(ApplicationDbContext db) : base(db)
        {
           _db = db;
        }

       
        public void Update(FoodType foodType)
        {
            var dbObject = _db.FoodType.FirstOrDefault(u => u.Id == foodType.Id);
            if(dbObject!=null)
            {
                dbObject.Name = foodType.Name;
            
            }
        }
    }
}
