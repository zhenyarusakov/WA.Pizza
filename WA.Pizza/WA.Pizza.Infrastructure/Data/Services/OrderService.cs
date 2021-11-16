using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Core.Interfaces;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class OrderService: IOrderService
    {
        private readonly IRepository<Order> _repository;
        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public Task<Order> GetOrderAsync(int id)
        {
            var order = _repository.GetById(id);

            if(order == null)
            {
                throw new ArgumentNullException($"There is no order with this {id}") ;
            }

            return order;
        }

        public Task<Order[]> GetOrdersAsync()
        {
            return _repository.GetAllAsync().ToArrayAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            await _repository.CreateAsync(order);

            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var localeOrder = await _repository.GetById(order.Id);

            if (localeOrder == null)
            {
                throw new ArgumentNullException($"There is no Order with this {order.Id}");
            }

            localeOrder.Name = order.Name;
            localeOrder.OrderItems = order.OrderItems;
            localeOrder.Status = order.Status;
            localeOrder.User = order.User;

            await _repository.UpdateAsync(localeOrder);

            return localeOrder;
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
