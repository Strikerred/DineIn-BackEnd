using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public partial class CustomerInfo
    {
        public CustomerInfo(sqlContext _context)
        {
            Orders = new HashSet<Orders>();
        }

        [PrimaryKey, AutoIncrement]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UsersEmail { get; set; }
        public string UserRole { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int? TransportationType { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
