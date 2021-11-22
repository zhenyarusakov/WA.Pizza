using Mapster;
using WA.Pizza.Core.Entities.BasketDomain;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;

namespace WA.Pizza.Infrastructure.Data.MapperConfiguration
{
    public static class MapperGlobal
    {
        public static void Configure()
        {
            TypeAdapterConfig<BasketItem, OrderItem>
                .NewConfig()
                .Ignore(x => x.Id);

            TypeAdapterConfig<Basket, Order>
                .NewConfig()
                .Ignore(x => x.Id)
                .Map(dst => dst.OrderItems, src => src.BasketItems);

            TypeAdapterConfig<OrderItem, OrderItemDto>.NewConfig();
            
            TypeAdapterConfig<Order, OrderDto>
                .NewConfig() 
                .Map(dst => dst.OrderItemDtos, src => src.OrderItems);
        }
    }
}
