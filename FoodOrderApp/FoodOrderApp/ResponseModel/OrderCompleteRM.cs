using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.ResponseModel
{
    public class OrderCompleteRM
    {
        public double OrderTotal { get; set; }
        public int CustomerId { get; set; }
        public int MenuItemId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
