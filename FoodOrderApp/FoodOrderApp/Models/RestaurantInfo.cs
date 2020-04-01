using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Models
{
    public class RestaurantInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        [Required]
        public String RestaurantName { get; set; }
        public String PhoneNumber { get; set; }

        public String Address { get; set; }
        public String Cuisine { get; set; }
    }
}
