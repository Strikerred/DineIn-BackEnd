using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class RestaurantInfo
    {
        public long RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public byte[] PhoneNumber { get; set; }
        public string Address { get; set; }
        public long? RestaurantInfoFoodCategory { get; set; }
    }
}
