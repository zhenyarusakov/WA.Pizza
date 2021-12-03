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
                .Ignore(
                    x => x.Id,
                    x => x.CatalogItem,
                    x=>x.OrderId,
                    x=>x.Order);

            TypeAdapterConfig<Basket, Order>
                .NewConfig()
                .Ignore(x => x.Id)
                .Map(dist => dist.OrderItems, s => s.BasketItems);

            TypeAdapterConfig<OrderItem, OrderItemDto>
                .NewConfig();

            TypeAdapterConfig<Order, OrderDto>
                .NewConfig()
                .Map(dst => dst.OrderItemDtos, src => src.OrderItems);


        }
    }
    //public class MappingRegistration : IRegister
    //{
    //    void IRegister.Register(TypeAdapterConfig config)
    //    {
    //        config
    //            .NewConfig<BasketItem, OrderItem>()
    //            .Ignore(
    //                x => x.Id,
    //                x => x.CatalogItem,
    //                x => x.OrderId,
    //                x => x.Order);

    //        config
    //            .NewConfig<Basket, Order>()
    //            .Ignore(x => x.Id)
    //            .Map(dist => dist.OrderItems, s => s.BasketItems);

    //        config
    //            .NewConfig<OrderItem, OrderItemDto>();

    //        config
    //            .NewConfig<Order, OrderDto>()
    //            .Map(dst => dst.OrderItemDtos, src => src.OrderItems);

    //    }
    //}
}
