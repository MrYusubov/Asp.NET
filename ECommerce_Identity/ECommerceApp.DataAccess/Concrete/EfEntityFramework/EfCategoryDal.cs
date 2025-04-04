﻿using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.Entities.Models;
using ECommerceApp.DataAccess.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.DataAccess.Concrete.EfEntityFramework
{
    public class EfCategoryDal : EFEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {
        public EfCategoryDal(NorthwindContext context) : base(context)
        {
        }
    }
}
