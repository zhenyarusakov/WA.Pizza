using System;
using System.Net;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Core.Interfaces;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO;
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

        public async Task<OrderDto> CreateOrderAsync(CreateOrUpdateOrderRequest orderRequest)
        {
            var createOrder = orderRequest.Adapt<Order>();

            await _repository.CreateAsync(createOrder);

            return createOrder.Adapt<OrderDto>();
        }

        public async Task<OrderDto> UpdateOrderAsync(CreateOrUpdateOrderRequest orderRequest)
        {
            var updateOrderDto = await _repository.GetById(orderRequest.Id);

            if (updateOrderDto == null)
            {
                throw new ArgumentNullException($"There is no Order with this {orderRequest.Id}");
            }

            TypeAdapter.Adapt(orderRequest, updateOrderDto);

            await _repository.UpdateAsync(updateOrderDto);

            return TypeAdapter.Adapt<OrderDto>(updateOrderDto);
        }

        public async Task UpdateOrderStatus(UpdateOrderStatusDto orderRequest)
        {
            var order = await _repository.GetById(orderRequest.OrderId);

            if (order == null)
            {
                throw new ArgumentNullException($"There is no Order with this {orderRequest.OrderId}");
            }

            order.Status = orderRequest.OrderStatus;

            await _repository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var deleteOrder = await _repository.GetById(id);

            if (deleteOrder == null)
            {
                throw new ArgumentNullException($"There is no Order with this {id}");
            }

            await _repository.DeleteAsync(deleteOrder);
        }
    }
}
