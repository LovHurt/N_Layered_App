using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concretes;

namespace DataAccess.Concretes
{
    public class EfProductDal : EfRepositoryBase<Product, Guid, ETradeContext>, IProductDal
    {
        public EfProductDal(ETradeContext context) : base(context)
        {

        }
    }
}
