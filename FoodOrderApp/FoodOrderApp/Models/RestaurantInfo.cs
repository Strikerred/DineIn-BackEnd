using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class RestaurantInfo
    {
        public RestaurantInfo()
        {
            MenuItems = new HashSet<MenuItems>();
        }

        public long RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public long? FoodCategoryId { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }
        public virtual ICollection<MenuItems> MenuItems { get; set; }
    }
}
