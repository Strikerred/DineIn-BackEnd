using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Orders = new HashSet<Orders>();
        }

        public int PaymentTypeId { get; set; }
        public string PaymentName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
