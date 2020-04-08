using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public partial class Orders
    {
        [PrimaryKey, AutoIncrement]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int? MenuItemId { get; set; }
        public double OrderTotal { get; set; }
        public int PaymentTypeId { get; set; }

        public virtual CustomerInfo Customer { get; set; }
        public virtual MenuItems MenuItem { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual ICollection<ItemSelected> ItemSelected { get; set; }
    }
}
