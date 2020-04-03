using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class CustomerInfo
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CustomerInfoAspNetUsers { get; set; }
        public byte[] PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
