using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Entities.Concretes;
using Core.DataAccess.Paging;

namespace Business.Concretes
{
    public class ProductManager: IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task Add(Product product)
        {
             await _productDal.AddAsync(product);
        }

        public async Task<IPaginate<Product>> GetListAsync()
        {
            return await _productDal.GetListAsync();
        }
    }
}
