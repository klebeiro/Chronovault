using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace chronovault_api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserCreateDTO> _userCreateValidator;
        private readonly IValidator<UserUpdateDTO> _userUpdateValidator;
        private readonly IValidator<UserCredentialDTO> _credentialValidator;

        /// <summary>
        /// Construtor do UserController.
        /// </summary>
        public UserController(
            IUserService userService,
            IValidator<UserCreateDTO> userCreateValidator,
            IValidator<UserUpdateDTO> userUpdateValidator,
            IValidator<UserCredentialDTO> credentialValidator)
        {
            _userService = userService;
            _userCreateValidator = userCreateValidator;
            _userUpdateValidator = userUpdateValidator;
            _credentialValidator = credentialValidator;
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário encontrado ou NotFound.</returns>
        /// <response code="200">Usuário encontrado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserResponseDTO>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        /// <response code="200">Lista de usuários retornada.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="dto">Dados para criação do usuário.</param>
        /// <returns>Usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos ou erro na criação.</response>
        [HttpPost("/")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserResponseDTO>> Create([FromBody] UserCreateDTO dto)
        {
            var validation = await _userCreateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var created = await _userService.CreateAsync(dto);
            if (created == null) return BadRequest("Could not create user.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="dto">Dados para atualização.</param>
        /// <returns>Usuário atualizado.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserResponseDTO>> Update(int id, [FromBody] UserUpdateDTO dto)
        {
            var validation = await _userUpdateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var updated = await _userService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Remove um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>NoContent se removido, NotFound se não existir.</returns>
        /// <response code="204">Usuário removido com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Realiza login do usuário.
        /// </summary>
        /// <param name="dto">Credenciais do usuário.</param>
        /// <returns>Ok se login for bem-sucedido, Unauthorized se falhar.</returns>
        /// <response code="200">Login realizado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserCredentialDTO dto)
        {
            var validation = await _credentialValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var isValid = await _userService.ValidateCredentialsAsync(dto.Email, dto.Password);
            if (!isValid) return Unauthorized("Invalid credentials.");
            // Aqui você pode retornar um token JWT, se desejar
            return Ok("Login successful.");
        }

        /// <summary>
        /// Altera a senha de um usuário.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <param name="newPassword">Nova senha.</param>
        /// <returns>Ok se alterada, NotFound se usuário não existir.</returns>
        /// <response code="200">Senha alterada com sucesso.</response>
        /// <response code="400">Senha inválida.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPost("change-password/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
                return BadRequest("Password must be at least 8 characters.");

            var changed = await _userService.ChangePasswordAsync(userId, newPassword);
            if (!changed) return NotFound();
            return Ok("Password changed.");
        }
    }
}