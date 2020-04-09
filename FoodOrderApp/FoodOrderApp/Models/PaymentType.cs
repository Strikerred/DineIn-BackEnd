using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Orders = new HashSet<Orders>();
        }

        [PrimaryKey, AutoIncrement]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentTypeId { get; set; }
        public string PaymentName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
