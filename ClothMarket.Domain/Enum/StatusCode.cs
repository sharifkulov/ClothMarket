using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.Domain.Enum
{
    
    public enum StatusCode
    {
        UserNotFound = 0,
        InternalServerError= 500,
        OK =200,
        ProductNotFound = 10
    }
}
