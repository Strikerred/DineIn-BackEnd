using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int MenuItemId { get; set; }
        public int OrderTotal { get; set; }
        public int PaymentTypeId { get; set; }

        public virtual CustomerInfo Customer { get; set; }
        public virtual MenuItems MenuItem { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
