using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class MenuItems
    {
        public MenuItems()
        {
            Orders = new HashSet<Orders>();
        }

        public int MenuItemId { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int RestaurantId { get; set; }
        public string ImageUrl { get; set; }
        public string MenuSection { get; set; }

        public virtual RestaurantInfo Restaurant { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
