using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Models
{
    public class ItemSelected
    {
        public int ItemSelectedId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
