using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public partial class RestaurantInfo
    {
        public RestaurantInfo()
        {
            MenuItems = new HashSet<MenuItems>();
        }

        [PrimaryKey, AutoIncrement]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int FoodCategoryId { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }
        public virtual ICollection<MenuItems> MenuItems { get; set; }
    }
}
