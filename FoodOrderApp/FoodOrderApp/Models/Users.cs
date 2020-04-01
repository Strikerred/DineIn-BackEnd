using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Models
{
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        public bool RememberMe { get; set; }
        public DateTime EmailVerifiedAt { get; set; }
        [Required]
        public String PhoneNumber { get; set; }
        [Required]
        public String Address { get; set; }
    }
}
