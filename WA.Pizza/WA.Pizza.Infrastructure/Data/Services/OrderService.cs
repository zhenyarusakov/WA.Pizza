using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Core.Interfaces;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var getOrder = await _repository.GetById(id);

            if (getOrder == null)
            {
                throw new ArgumentNullException($"There is no Order with this {id}");
            }
            
            return getOrder.Adapt<OrderDto>();
        }

        public Task<OrderDto[]> GetOrdersAsync()
        {
            return _repository.GetAllAsync().ProjectToType<OrderDto>().ToArrayAsync();
        }

        public async Task<OrderDto> CreateOrderAsync(OrderForModifyDto orderModify)
        {
            var createOrder = orderModify.Adapt<Order>();

            await _repository.CreateAsync(createOrder);

            return createOrder.Adapt<OrderDto>();
        }

    }
}
