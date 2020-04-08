using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Models
{
    public class SelectedItem
    {
        public int SelectedItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }

        public virtual MenuItems MenuItems { get; set; }
        public virtual Orders Orders { get; set; }

    }
}
