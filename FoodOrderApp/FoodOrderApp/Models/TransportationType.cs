using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class TransportationType
    {
        public int TransportationId { get; set; }
        public string TransportationName { get; set; }
        public string DriversLicense { get; set; }
        public bool IsVerified { get; set; }
    }
}
