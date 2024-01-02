using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace Business
{
    public static class ValidateEntityAttributeExtensions
    {
        public static void ValidateEntity(ControllerBase controller)
        {
            var action = controller.ControllerContext.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                return;
            }

            var validateAttributes = action.MethodInfo.GetCustomAttributes(typeof(ValidateEntityAttribute), true);

            foreach (var attribute in validateAttributes.Cast<ValidateEntityAttribute>())
            {
                var validator = Activator.CreateInstance(attribute.ValidatorType) as IValidator;
                if (validator == null)
                {
                    throw new InvalidOperationException($"Validator instance could not be created for {attribute.ValidatorType}.");
                }

                var entityType = validator.GetType().BaseType?.GenericTypeArguments[0];

                var parameter = action.Parameters.SingleOrDefault(p => p.ParameterType == entityType);

                var validationContext = new ValidationContext<object>(parameter);
                var validationResult = validator.Validate(validationContext);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
        }
    }
}
