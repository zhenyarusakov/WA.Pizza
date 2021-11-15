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

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await _repository.GetById(id);

            if(order == null)
            {
                throw new ArgumentNullException($"There is no order with this {id}") ;
            }

            return order;
        }

        public async Task<Order[]> GetOrdersAsync()
        {
            var orders = await _repository.GetAllAsync().ToArrayAsync();

            return orders;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            await _repository.CreateAsync(order);

            return order;
        }

        public async Task<Order> UpdateOrderAsync(int id)
        {
            var updateOrder = await _repository.GetById(id);

            if (updateOrder == null)
            {
                throw new ArgumentNullException($"There is no Order with this {id}");
            }
            
            await _repository.UpdateAsync(updateOrder);

            return updateOrder;
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
