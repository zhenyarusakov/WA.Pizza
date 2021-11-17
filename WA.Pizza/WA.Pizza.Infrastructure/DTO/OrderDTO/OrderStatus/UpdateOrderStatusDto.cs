using System.Text.Json.Serialization;
using WA.Pizza.Core.Entities.OrderDomain;

namespace WA.Pizza.Infrastructure.DTO
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
