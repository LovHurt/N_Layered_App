using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class ValidationMessages
    {
        public static string ProductNameMustNotBeEmpty = "Product name can not be empty";
        public static string ProductPriceMustBeHigherThanZero = "Product's unit price must be higher than zero";
    }
}
