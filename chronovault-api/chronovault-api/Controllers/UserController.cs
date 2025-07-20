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
    /// Controller responsável pelo gerenciamento de usuários.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IValidator<UserCreateDTO> _userCreateValidator;
        private readonly IValidator<UserCredentialDTO> _credentialValidator;

        /// <summary>
        /// Construtor do UserController.
        /// </summary>
        public UserController(
            IUserService userService,
            IJwtService jwtService,
            IValidator<UserCreateDTO> userCreateValidator,
            IValidator<UserCredentialDTO> credentialValidator)
        {
            _userService = userService;
            _userCreateValidator = userCreateValidator;
            _credentialValidator = credentialValidator;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário encontrado ou NotFound.</returns>
        /// <response code="200">Usuário encontrado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}/details")]
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
        [HttpGet("get-all")]
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
        [HttpPost("create")]
        [AllowAnonymous]
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
        /// Realiza login do usuário.
        /// </summary>
        /// <param name="dto">Credenciais do usuário.</param>
        /// <returns>Ok se login for bem-sucedido, Unauthorized se falhar.</returns>
        /// <response code="200">Login realizado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserCredentialDTO dto)
        {
            var validation = await _credentialValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var authReturn = await _jwtService.GenerateToken(dto);
            if (string.IsNullOrEmpty(authReturn.Token)) return Unauthorized("Invalid credentials.");

            return Ok(authReturn);
        }
    }
}