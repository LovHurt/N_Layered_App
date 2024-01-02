using Business.Dtos.Requests;
using Business.Messages;
using Entities.Concretes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace Business.Rules.ValidationRules
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithErrorCode(ValidationMessages.ProductNameMustNotBeEmpty);
;
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithErrorCode(ValidationMessages.ProductPriceMustBeHigherThanZero);
        }
    }
}
