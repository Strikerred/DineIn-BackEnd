using FoodOrderApp.Models;
using FoodOrderApp.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Repositories
{
    public class PaymentRepo
    {
        private sqlContext _context;
        private IConfiguration _config;

        public PaymentRepo(sqlContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Tuple<bool, object>> Order(OrderRM orderRM, string userName)
        {
            Random random = new Random();
            var items = new List<int>();

            //PaymentTypeId 3 is pay by cash
            if (orderRM.PaymentTypeId != 3)
            {
                StripeConfiguration.ApiKey = _config["Stripe_SecretKey"];

                var optionsToken = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = orderRM.CardNumber,
                        ExpMonth = orderRM.Month,
                        ExpYear = orderRM.Year,
                        Cvc = orderRM.Cvc
                    }
                };

                if (optionsToken == null)
                {
                    return new Tuple<bool, object>(false, "Payment cannot be completed");
                }

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(optionsToken);

                if (stripeToken == null)
                {
                    return new Tuple<bool, object>(false, "Payment cannot be completed");
                }

                var options = new ChargeCreateOptions
                {
                    Amount = orderRM.Amount * 100,
                    Currency = "usd",
                    Source = stripeToken.Id,
                    Description = $"Success final project payment by {userName}"
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (!charge.Paid)
                {
                    return new Tuple<bool, object>(false, "Payment cannot be completed");
                }
            }
          
            var order = new Orders
            {
                CustomerId = orderRM.CustomerId,
                OrderTotal = orderRM.Amount,
                PaymentTypeId = orderRM.PaymentTypeId,
            };

            var randomId = random.Next(10000);

            while (_context.Orders.Any(id => id.OrderId == randomId))
            {
                randomId = random.Next(10000);
            }

            order.OrderId = randomId;

            _context.Orders.Add(order);

            foreach (int item in orderRM.MenuItems)
            {
                var StoreItem = new SelectedItem { OrderId = randomId, MenuItemId = item };
                items.Add(item);
                _context.SelectedItem.Add(StoreItem);
            }

            await _context.SaveChangesAsync();

            var returnOrder = new OrderCompleteRM
            {
                OrderId = order.OrderId,
                Customer = userName,
                MenuItems = items,
                OrderTotal = order.OrderTotal,
                PaymentTypeId = order.PaymentTypeId                
            };
           
            return new Tuple<bool, object>(true, returnOrder);
        }
    }
}
