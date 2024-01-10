using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes
{
    public class EfUserDal : EfRepositoryBase<User, int, NorthwindContext>, IUserDal
    {
        public EfUserDal(NorthwindContext context) : base(context)
        {

        }
     

    }
}
