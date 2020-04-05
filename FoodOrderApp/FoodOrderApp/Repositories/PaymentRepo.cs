using FoodOrderApp.Models;
using FoodOrderApp.ResponseModel;
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
                MenuItemId = orderRM.MenuItemId,
                OrderTotal = orderRM.Amount,
                PaymentTypeId = orderRM.PaymentTypeId,
            };

            _context.Orders.Add(order);

            var returnOrder = new OrderCompleteRM
            {
                CustomerId = order.CustomerId,
                MenuItemId = order.MenuItemId,
                OrderTotal = order.OrderTotal,
                PaymentTypeId = order.PaymentTypeId                
            };

            await _context.SaveChangesAsync();

            return new Tuple<bool, object>(true, returnOrder);
        }
    }
}
