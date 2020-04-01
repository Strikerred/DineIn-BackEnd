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
        [ForeignKey("MenuItemId")]
        public virtual MenuItems MenuItemId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

    }
}
