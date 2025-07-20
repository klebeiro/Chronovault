using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace chronovault_api.Controllers
{
    /// <summary>
    /// Controller para gerenciar pedidos.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderCreateDTO> _orderCreateValidator;

        /// <summary>
        /// Construtor do controller de pedidos.
        /// </summary>
        /// <param name="orderService">Serviço de pedidos.</param>
        /// <param name="orderCreateValidator">Validador para a criação de pedidos.</param>
        /// <param name="orderUpdateValidator">Validador para a atualização de pedidos.</param>
        public OrderController(
            IOrderService orderService,
            IValidator<OrderCreateDTO> orderCreateValidator)
        {
            _orderService = orderService;
            _orderCreateValidator = orderCreateValidator;
        }

        /// <summary>
        /// Obtém um pedido por ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>O pedido encontrado ou NotFound se não existir.</returns>
        /// <response code="200">Retorna o pedido encontrado.</response>
        /// <response code="404">Se o pedido não for encontrado.</response>
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(OrderResponseDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponseDetailsDTO>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        /// <summary>
        /// Obtém todos os pedidos.
        /// </summary>
        /// <returns>Uma lista de todos os pedidos.</returns>
        /// <response code="200">Retorna a lista de pedidos.</response>
        [HttpGet("from-user/{id}")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetAllByUserId(int id)
        {
            var orders = await _orderService.GetAllByUserIdAsync(id);
            return Ok(orders);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="dto">Dados para a criação do pedido.</param>
        /// <returns>O pedido criado ou BadRequest se a criação falhar.</returns>
        /// <response code="201">Retorna o pedido criado.</response>
        /// <response code="400">Se a validação falhar ou a criação não for possível.</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(OrderResponseDetailsDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderResponseDetailsDTO>> Create([FromBody] OrderCreateDTO dto)
        {
            var validation = await _orderCreateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var created = await _orderService.CreateAsync(dto, userId);
            if (created == null) return BadRequest("Could not create order.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}