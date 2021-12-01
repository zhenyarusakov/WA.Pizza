using System.Collections.Generic;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers
{
    public class OrderHelper
    {
        public static ICollection<Order> CreateListOfFilledOrders()
        {
            List<Order> orders = new List<Order>();

            orders.Add(new Order
            {
                Status = OrderStatus.Dispatch,
                UserId = 1,
                OrderItems = new List<OrderItem>
                {
                    new()
                    {
                        Quantity = 2,
                        Name = "Dispatch",
                        Description = "Dispatch",
                        Price = 12
                    }
                }
            });

            orders.Add(new Order
            {
                Status = OrderStatus.InProcessing,
                UserId = 1,
                OrderItems = new List<OrderItem>
                {
                    new ()
                    {
                        Quantity = 3,
                        Name = "InProcessing",
                        Description = "InProcessing",
                        Price = 13
                    }
                }
            });

            return orders;
        }

        public static Order CreateOneFilledOrders()
        {
            return new Order
            {
                Status = OrderStatus.Dispatch,
                OrderItems = new List<OrderItem>
                {
                    new()
                    {
                        Quantity = 2,
                        Name = "qwe",
                        Description = "qwe",
                        Price = 12
                    }
                }
            };
        }
    }
}
