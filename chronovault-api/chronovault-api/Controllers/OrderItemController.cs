using Microsoft.AspNetCore.Mvc;
using chronovault_api.Services.Interfaces;
using chronovault_api.DTOs.Response;
using System.Net;

namespace chronovault_api.Controllers
{
    /// <summary>
    /// Controller para gerenciar itens de pedido.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        /// <summary>
        /// Construtor do controller de itens de pedido.
        /// </summary>
        /// <param name="orderItemService">Serviço de itens de pedido.</param>
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        /// <summary>
        /// Obtém um item de pedido por ID.
        /// </summary>
        /// <param name="id">ID do item de pedido.</param>
        /// <returns>O item de pedido encontrado ou NotFound se não existir.</returns>
        /// <response code="200">Retorna o item de pedido encontrado.</response>
        /// <response code="404">Se o item de pedido não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderItemResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderItemResponseDTO>> GetById(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound($"Order item with ID {id} not found.");

            return Ok(orderItem);
        }

        /// <summary>
        /// Obtém todos os itens de pedido associados a um pedido.
        /// </summary>
        /// <param name="orderId">ID do pedido.</param>
        /// <returns>Uma lista de itens de pedido associados ao pedido.</returns>
        /// <response code="200">Retorna a lista de itens de pedido.</response>
        [HttpGet("order/{orderId}")]
        [ProducesResponseType(typeof(IEnumerable<OrderItemResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItemResponseDTO>>> GetByOrderId(int orderId)
        {
            var orderItems = await _orderItemService.GetByOrderIdAsync(orderId);
            return Ok(orderItems);
        }
    }
}