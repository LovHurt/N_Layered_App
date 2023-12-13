using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class BusinessMessages
    {
        public static string CategoryLimit = "Category count can be max 10!";
        public static string ProductLimitPerCategory = "Product Count oer Category can't exceed 20!";
        public static string MaxCustomerTenPerCity = "Max Customer number is 10 per city, it can't be exceeded";
        public static string ContactNameCantRepeat = "Contact name can't be same";
    }
}
