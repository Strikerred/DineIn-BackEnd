using FoodOrderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.ResponseModel
{
    public class ItemsRM
    {
        public MenuItems Item { get; set; }
        public int qty { get; set; }
    }
}
