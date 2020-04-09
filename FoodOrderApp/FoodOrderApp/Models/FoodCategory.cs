using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public partial class FoodCategory
    {
        public FoodCategory()
        {
            RestaurantInfo = new HashSet<RestaurantInfo>();
        }

        [PrimaryKey, AutoIncrement]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodCategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<RestaurantInfo> RestaurantInfo { get; set; }
    }
}
