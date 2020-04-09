using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public partial class TransportationType
    {
        [PrimaryKey, AutoIncrement]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransportationId { get; set; }
        public string TransportationName { get; set; }
        public string DriversLicense { get; set; }
        public int IsVerified { get; set; }
    }
}
