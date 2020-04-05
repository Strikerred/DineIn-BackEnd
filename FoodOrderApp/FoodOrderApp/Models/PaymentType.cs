using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentTypeId { get; set; }
        public string PaymentName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
