using FoodOrderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Repositories
{
    public class CustomerRepo
    {
        private sqlContext _context;

        public CustomerRepo(sqlContext context)
        {
            _context = context;
        }

        public int GetCustomer(string username)
        {
            var user = _context.CustomerInfo.SingleOrDefault(u => u.UsersEmail == username);

            return user.CustomerId;
        }
    }
}
