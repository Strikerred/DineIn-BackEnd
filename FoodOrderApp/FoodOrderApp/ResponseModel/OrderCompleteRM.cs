using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.ResponseModel
{
    public class OrderCompleteRM
    {
        public int OrderId { get; set; }
        public double OrderTotal { get; set; }
        public string Customer { get; set; }
        public IEnumerable<int> MenuItems { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
