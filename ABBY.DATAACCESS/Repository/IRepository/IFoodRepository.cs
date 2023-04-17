using ABBY.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBY.DATAACCESS.Repository.IRepository
{

    public interface IFoodRepository : IRepository<FoodType>
    {
        void Update(FoodType foodType);

    }
}
