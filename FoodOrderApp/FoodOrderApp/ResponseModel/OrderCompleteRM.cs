using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.ResponseModel
{
    public class OrderCompleteRM
    {
        public double OrderTotal { get; set; }
        public long CustomerId { get; set; }
        public long MenuItemId { get; set; }
        public long PaymentTypeId { get; set; }
    }
}
