using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace chronovault_api.Controllers
{
    /// <summary>
    /// Controller para gerenciar pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderCreateDTO> _orderCreateValidator;
        private readonly IValidator<OrderUpdateDTO> _orderUpdateValidator;

        /// <summary>
        /// Construtor do controller de pedidos.
        /// </summary>
        /// <param name="orderService">Serviço de pedidos.</param>
        /// <param name="orderCreateValidator">Validador para a criação de pedidos.</param>
        /// <param name="orderUpdateValidator">Validador para a atualização de pedidos.</param>
        public OrderController(
            IOrderService orderService,
            IValidator<OrderCreateDTO> orderCreateValidator,
            IValidator<OrderUpdateDTO> orderUpdateValidator)
        {
            _orderService = orderService;
            _orderCreateValidator = orderCreateValidator;
            _orderUpdateValidator = orderUpdateValidator;
        }

        /// <summary>
        /// Obtém um pedido por ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>O pedido encontrado ou NotFound se não existir.</returns>
        /// <response code="200">Retorna o pedido encontrado.</response>
        /// <response code="404">Se o pedido não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponseDTO>> GetById(int id)
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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="dto">Dados para a criação do pedido.</param>
        /// <returns>O pedido criado ou BadRequest se a criação falhar.</returns>
        /// <response code="201">Retorna o pedido criado.</response>
        /// <response code="400">Se a validação falhar ou a criação não for possível.</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderResponseDTO>> Create([FromBody] OrderCreateDTO dto)
        {
            var validation = await _orderCreateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var created = await _orderService.CreateAsync(dto);
            if (created == null) return BadRequest("Could not create order.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um pedido existente.
        /// </summary>
        /// <param name="id">ID do pedido a ser atualizado.</param>
        /// <param name="dto">Dados para a atualização do pedido.</param>
        /// <returns>O pedido atualizado ou NotFound se não existir.</returns>
        /// <response code="200">Retorna o pedido atualizado.</response>
        /// <response code="400">Se a validação falhar.</response>
        /// <response code="404">Se o pedido não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponseDTO>> Update(int id, [FromBody] OrderUpdateDTO dto)
        {
            var validation = await _orderUpdateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var updated = await _orderService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Exclui um pedido por ID.
        /// </summary>
        /// <param name="id">ID do pedido a ser excluído.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida ou NotFound se não existir.</returns>
        /// <response code="204">Se o pedido for excluído com sucesso.</response>
        /// <response code="404">Se o pedido não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _orderService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}