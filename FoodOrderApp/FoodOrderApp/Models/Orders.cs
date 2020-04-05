using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class Orders
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public long MenuItemId { get; set; }
        public long OrderTotal { get; set; }
        public long PaymentTypeId { get; set; }

        public virtual CustomerInfo Customer { get; set; }
        public virtual MenuItems MenuItem { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
