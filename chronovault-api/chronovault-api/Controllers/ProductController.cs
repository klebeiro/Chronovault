using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace chronovault_api.Controllers
{
    /// <summary>
    /// Controller para gerenciar produtos.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductCreateDTO> _productCreateValidator;

        /// <summary>
        /// Construtor do controller de produtos.
        /// </summary>
        /// <param name="productService">Serviço de produtos.</param>
        /// <param name="productCreateValidator">Validador para a criação de produtos.</param>
        /// <param name="productUpdateValidator">Validador para a atualização de produtos.</param>
        public ProductController(
            IProductService productService,
            IValidator<ProductCreateDTO> productCreateValidator)
        {
            _productService = productService;
            _productCreateValidator = productCreateValidator;
        }

        /// <summary>
        /// Obtém um produto por ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>O produto encontrado ou NotFound se não existir.</returns>
        /// <response code="200">Retorna o produto encontrado.</response>
        /// <response code="404">Se o produto não for encontrado.</response>
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponseDTO>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        /// <returns>Uma lista de todos os produtos.</returns>
        /// <response code="200">Retorna a lista de produtos.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="dto">Dados para a criação do produto.</param>
        /// <returns>O produto criado ou BadRequest se a criação falhar.</returns>
        /// <response code="201">Retorna o produto criado.</response>
        /// <response code="400">Se a validação falhar ou a criação não for possível.</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ProductResponseDTO>> Create([FromBody] ProductCreateDTO dto)
        {
            var validation = await _productCreateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var created = await _productService.CreateAsync(dto);
            if (created == null) return BadRequest("Could not create product.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}