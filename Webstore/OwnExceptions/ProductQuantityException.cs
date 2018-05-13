using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore.OwnExceptions
{
    public class ProductQuantityException : Exception
    {
        public ProductQuantityException(string message) : base($"Less product is in store than the order quantity: " + message)
        {
        }
    }
}
