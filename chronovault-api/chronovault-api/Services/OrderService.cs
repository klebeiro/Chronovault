using AutoMapper;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Models;
using chronovault_api.Models.Enums;
using chronovault_api.Models.ValueObjects;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;

namespace chronovault_api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponseDetailsDTO?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order != null ? _mapper.Map<OrderResponseDetailsDTO>(order) : null;
        }
        public async Task<List<OrderResponseDTO>> GetAllByUserIdAsync(int id)
        {
            var orders = await _orderRepository.GetAllByUserIdAsync(id);
            return orders != null ? _mapper.Map<List<OrderResponseDTO>>(orders) : new List<OrderResponseDTO>();
        }

        public async Task<OrderResponseDetailsDTO?> CreateAsync(OrderCreateDTO orderCreateDTO, int userId)
        {
            var order = _mapper.Map<Order>(orderCreateDTO);
            order.UserId = userId;
            order.ShippingAddress = _mapper.Map<Address>(orderCreateDTO.ShippingAddress);

            order.TotalAmount = order.OrderItems.Sum(item => item.Price * item.Quantity);

            var created = await _orderRepository.CreateAsync(order);
            return created != null ? _mapper.Map<OrderResponseDetailsDTO>(created) : null;
        }
    }
}