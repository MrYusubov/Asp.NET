using AirBnB.Core.DataAccess.EntityFramework;
using AirBnB.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnB.DataAccess.Abstraction
{
    public interface IHouseDal:IEntityRepository<House>
    {
    }
}
