using System;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Core.Interfaces;
using WA.Pizza.Infrastructure.Abstractions;
using WA.Pizza.Infrastructure.DTO.OrderDTO.Order;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class OrderService: IOrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IMapper _mapper;
        public OrderService(IRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var orderDto = await _repository.GetById(id);

            if(orderDto == null)
            {
                throw new ArgumentNullException($"There is no order with this {id}") ;
            }

            return _mapper.Map<OrderDto>(orderDto);
        }

        public Task<OrderDto[]> GetOrdersAsync()
        {
            return _repository.GetAllAsync().ProjectToType<OrderDto>().ToArrayAsync();
        }

        public async Task<OrderDto> CreateOrderAsync(OrderForModifyDto modifyDto)
        {
            var orderDto = _mapper.Map<Order>(modifyDto);

            await _repository.CreateAsync(orderDto);

            return _mapper.Map<OrderDto>(orderDto);
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderForModifyDto modifyDto)
        {
            var updateeOrderDto = await _repository.GetById(modifyDto.Id);

            if (updateeOrderDto == null)
            {
                throw new ArgumentNullException($"There is no Order with this {modifyDto.Id}");
            }

            _mapper.Map(modifyDto, updateeOrderDto);

            await _repository.UpdateAsync(updateeOrderDto);

            return _mapper.Map<OrderDto>(updateeOrderDto);
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
