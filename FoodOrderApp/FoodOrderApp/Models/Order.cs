using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class Order
    {
        public long OrderId { get; set; }
        public long OrderCustomerInfo { get; set; }
        public long OrderMenuItem { get; set; }
        public byte[] OrderTotal { get; set; }
        public long OrderPaymentType { get; set; }
    }
}
