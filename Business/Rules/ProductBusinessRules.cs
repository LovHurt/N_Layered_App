using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Messages;
using Core.Business.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstract;

namespace Business.Rules
{
    public class ProductBusinessRules: BaseBusinessRules
    {
        IProductDal _productDal;

        public ProductBusinessRules(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task MaxProductCountPerCategoryTwenty(int categoryId)
        {
            var result = await _productDal.GetListAsync(
                p=>p.CategoryId==categoryId,
                size: 25
                );

            if (result.Count >= 20)
            {
                throw new BusinessException(BusinessMessages.ProductLimitPerCategory);
            }
        }
    }
}
