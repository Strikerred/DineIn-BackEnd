using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Models
{
    public class Orders
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public ICollection<MenuItems> MenuItems { get; set; }
        [Required]
        public String OrdererFirstName { get; set; }
        [Required]
        public String OrdererEmail { get; set; }
        [Required]
        public String OrdererPhoneNumber { get; set; }
        [Required]
        public decimal TotalOrderPrice { get; set; }

    }
}
