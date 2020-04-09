using FoodOrderApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.ResponseModel
{
    public class OrderRM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? CardNumber { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string? Cvc { get; set; }
        public long Amount { get; set; }
        public int CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public IEnumerable<ItemsRM> itemsRM { get; set; }
    }
}
