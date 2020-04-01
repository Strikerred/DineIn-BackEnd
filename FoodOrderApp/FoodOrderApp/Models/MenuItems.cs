using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Models
{
    public class MenuItems
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuItemId { get; set; }
        [Required]
        public String DishName { get; set; }
        public String Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public String RestaurantName { get; set; }

    }
}
