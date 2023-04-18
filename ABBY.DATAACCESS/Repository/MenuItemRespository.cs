using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBY.DATAACCESS.Repository
{
    public class MenuItemRepository : Repository<MenuItem> , IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
           _db = db;
        }

       
        public void Update(MenuItem menuItem)
        {
            var dbObject = _db.MenuItem.FirstOrDefault(u => u.Id == menuItem.Id);
            if(dbObject!=null)
            {
                dbObject.Name = menuItem.Name;
                dbObject.Description =  menuItem.Description;
                dbObject.Price =  menuItem.Price;
                dbObject.FoodTypeId =  menuItem.FoodTypeId;
                if(menuItem.Image !=null)
                {
                    dbObject.Image = menuItem.Image;
                }
            }
        }
    }
}
