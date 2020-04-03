using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class MenuItem
    {
        public long MenuItemId { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public byte[] Price { get; set; }
        public long MenuItemRestaurantInfo { get; set; }
    }
}
