using AutoMapper;
using chronovault_api.DTOs.Response;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;

namespace chronovault_api.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderItemResponseDTO?> GetByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            return orderItem != null ? _mapper.Map<OrderItemResponseDTO>(orderItem) : null;
        }

        public async Task<IEnumerable<OrderItemResponseDTO>> GetByOrderIdAsync(int orderId)
        {
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderItemResponseDTO>>(orderItems);
        }
    }
}