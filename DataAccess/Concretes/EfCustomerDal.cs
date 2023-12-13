﻿using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfCustomerDal : EfRepositoryBase<Customer, string, NorthwindContext>, ICustomerDal
    {
        public EfCustomerDal(NorthwindContext context) : base(context)
        {

        }
    }
}