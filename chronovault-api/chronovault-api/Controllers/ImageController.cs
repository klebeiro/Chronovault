using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace chronovault_api.Controllers
{
    /// <summary>
    /// Controller para gerenciar imagens de produtos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IValidator<ImageCreateDTO> _imageCreateValidator;
        private readonly IValidator<ImageUpdateDTO> _imageUpdateValidator;

        /// <summary>
        /// Construtor do controller de imagens.
        /// </summary>
        /// <param name="imageService">Serviço de imagens.</param>
        /// <param name="imageCreateValidator">Validador para a criação de imagens.</param>
        /// <param name="imageUpdateValidator">Validador para a atualização de imagens.</param>
        public ImageController(
            IImageService imageService,
            IValidator<ImageCreateDTO> imageCreateValidator,
            IValidator<ImageUpdateDTO> imageUpdateValidator)
        {
            _imageService = imageService;
            _imageCreateValidator = imageCreateValidator;
            _imageUpdateValidator = imageUpdateValidator;
        }

        /// <summary>
        /// Obtém uma imagem por ID.
        /// </summary>
        /// <param name="id">ID da imagem.</param>
        /// <returns>A imagem encontrada ou NotFound se não existir.</returns>
        /// <response code="200">Retorna a imagem encontrada.</response>
        /// <response code="404">Se a imagem não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ImageResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ImageResponseDTO>> GetById(int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null) return NotFound();
            return Ok(image);
        }

        /// <summary>
        /// Obtém todas as imagens associadas a um produto.
        /// </summary>
        /// <param name="productId">ID do produto.</param>
        /// <returns>Uma lista de imagens associadas ao produto.</returns>
        /// <response code="200">Retorna a lista de imagens do produto.</response>
        [HttpGet("product/{productId}")]
        [ProducesResponseType(typeof(IEnumerable<ImageResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ImageResponseDTO>>> GetByProductId(int productId)
        {
            var images = await _imageService.GetByProductIdAsync(productId);
            return Ok(images);
        }

        /// <summary>
        /// Cria uma nova imagem.
        /// </summary>
        /// <param name="dto">Dados para a criação da imagem.</param>
        /// <returns>A imagem criada ou BadRequest se a criação falhar.</returns>
        /// <response code="201">Retorna a imagem criada.</response>
        /// <response code="400">Se a validação falhar ou a criação não for possível.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ImageResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ImageResponseDTO>> Create([FromBody] ImageCreateDTO dto)
        {
            var validation = await _imageCreateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var created = await _imageService.CreateAsync(dto);
            if (created == null) return BadRequest("Could not create image.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza uma imagem existente.
        /// </summary>
        /// <param name="id">ID da imagem a ser atualizada.</param>
        /// <param name="dto">Dados para a atualização da imagem.</param>
        /// <returns>A imagem atualizada ou NotFound se não existir.</returns>
        /// <response code="200">Retorna a imagem atualizada.</response>
        /// <response code="400">Se a validação falhar.</response>
        /// <response code="404">Se a imagem não for encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ImageResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ImageResponseDTO>> Update(int id, [FromBody] ImageUpdateDTO dto)
        {
            var validation = await _imageUpdateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var updated = await _imageService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Exclui uma imagem por ID.
        /// </summary>
        /// <param name="id">ID da imagem a ser excluída.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida ou NotFound se não existir.</returns>
        /// <response code="204">Se a imagem for excluída com sucesso.</response>
        /// <response code="404">Se a imagem não for encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _imageService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Exclui todas as imagens associadas a um produto.
        /// </summary>
        /// <param name="productId">ID do produto.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida ou NotFound se não existir.</returns>
        /// <response code="204">Se as imagens forem excluídas com sucesso.</response>
        /// <response code="404">Se as imagens não forem encontradas.</response>
        [HttpDelete("product/{productId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteByProductId(int productId)
        {
            var deleted = await _imageService.DeleteByProductIdAsync(productId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}