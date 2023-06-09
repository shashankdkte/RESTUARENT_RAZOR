﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBY.DATAACCESS.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        IFoodRepository FoodType { get; }
        IMenuItemRepository MenuItem { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderHeaderRespository OrderHeader { get; }
        IOrderDetailsRespository OrderDetails { get; }
        IApplicationUserRepository ApplicationUser { get; }

        void Save();
    }
}
