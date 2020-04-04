﻿using System;
using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public partial class FoodCategory
    {
        public FoodCategory()
        {
            RestaurantInfo = new HashSet<RestaurantInfo>();
        }

        public long FoodCategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<RestaurantInfo> RestaurantInfo { get; set; }
    }
}
