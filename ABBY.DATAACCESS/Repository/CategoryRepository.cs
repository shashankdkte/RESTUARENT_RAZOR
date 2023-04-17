using ABBY.DATAACCESS.Repository.IRepository;
using ABBY.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBY.DATAACCESS.Repository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
           _db = db;
        }

       
        public void Update(Category category)
        {
            var dbObject = _db.Category.FirstOrDefault(u => u.Id == category.Id);
            if(dbObject!=null)
            {
                dbObject.Name = category.Name;
                dbObject.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
