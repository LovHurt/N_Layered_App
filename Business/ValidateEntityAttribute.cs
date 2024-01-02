using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    // Attribute
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateEntityAttribute : Attribute
    {
        public Type ValidatorType { get; }

        public ValidateEntityAttribute(Type validatorType)
        {
            ValidatorType = validatorType;
        }
    }

}
