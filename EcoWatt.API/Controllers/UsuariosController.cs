using EcoWatt.Model;
using EcoWatt.Model.DTO.Request;
using EcoWatt.Model.DTO.Response;
using EcoWatt.Repository;
using EcoWatt.Service.ConvertMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcoWatt.API.Controllers
{

    /// <summary>
    /// Controlador responsável por gerenciar usuarios.
    /// </summary>
    [Route("api/usuarios/[controller]")]
    [ApiController]
    [Tags("Usuarios")]
    public class UsuariosController(IRepository<Usuario> usuarioRepository) : ControllerBase
    {
        private readonly IRepository<Usuario> _usuarioRepository = usuarioRepository;
        private readonly UsuarioService _usuarioService = new UsuarioService();


        /// <summary>
        /// Retorna a lista completa de usuarios.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     GET /usuario
        /// 
        /// Esse endpoint retorna uma lista de todos os usuarios disponíveis no sistema.
        /// </remarks>
        /// <response code="200">Retorna a lista de usuarios</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
        {
            IEnumerable<Usuario> usuarios = await _usuarioRepository.GetAll();

            //Retorna um enumerable de usuarios sem os dados sensíveis.
            return Ok(_usuarioService.usuariosToResponseIEnumerable(usuarios));

        }

        /// <summary>
        /// Retorna um usuario com base no id fornecido.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     GET /usuario/id
        /// 
        /// Esse endpoint retorna um usuario com base no id fornecido.
        /// </remarks>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Nenhum usuario encontrado</response>
        /// <response code="500">Erro interno no servidor</response>
        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {

            var usuario = await _usuarioRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            //Retorna o usuario sem seus dados sensíveis.
            return Ok(_usuarioService.usuarioToResponse(usuario));
        }

        /// <summary>
        /// Cria um novo usuario.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     POST /usuario
        /// 
        /// Esse endpoint salva os dados do usuario no banco de dados.
        /// </remarks>
        /// <response code="200">Usuario criado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Erro de servidor</response>
        /// <response code="401">Usuário não autorizado</response>
        // POST api/<UsuariosController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post(UsuarioRequest usuarioRequest)
        {
            Usuario usuario = new Usuario();
            UsuarioResponse usuarioResponse = new UsuarioResponse();

            usuario = _usuarioService.requestToUsuario(usuarioRequest);

            if (usuario == null)
            {
                return BadRequest();
            }

            await _usuarioRepository.Add(usuario,0);
            usuarioResponse = _usuarioService.usuarioToResponse(usuario);

            // Retorna o usuario sem seus dados sensíveis.
            return Ok(usuarioResponse);
        }

        /// <summary>
        /// Atualiza um usuario com base no id fornecido.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     PUT /usuario/{id}
        /// 
        /// Esse endpoint atualiza o usuario com base no id fornecido.
        /// </remarks>
        /// <response code="200">Usuario atualizado</response>
        /// <response code="404">Nenhum usuario encontrado</response>
        /// <response code="500">Erro interno no servidor</response>
        /// <response code="401">Usuário não autorizado</response>
        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioRequest usuarioRequest)
        {

            Usuario usuario = new Usuario();
            UsuarioResponse usuarioResponse = new UsuarioResponse();

            usuario = _usuarioService.requestToUsuario(usuarioRequest);
            usuario.IdUsuario = id;

            Usuario existingUsuario = await _usuarioRepository.GetById(id);

            if (existingUsuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.Update(id, usuario);
            usuarioResponse = _usuarioService.usuarioToResponse(usuario);

            // Retorna o usuario com seus dados alterados, sem seus dados sensíveis.
            return Ok(usuarioResponse);
        }

        /// <summary>
        /// Deleta um usuario com base no id fornecido.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     DELETE/usuario/{id}
        /// 
        /// Esse endpoint deleta o usuario com base no id fornecido.
        /// </remarks>
        /// <response code="200">Usuario deletado</response>
        /// <response code="404">Nenhum usuario encontrado</response>
        /// <response code="500">Erro interno no servidor</response>
        /// <response code="401">Usuário não autorizado</response>
        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            
            var usuario = await _usuarioRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.Delete(id);

            //Retorna nada.
            return Ok();
        }

    }
}
