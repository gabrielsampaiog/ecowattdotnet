using EcoWatt.Model.DTO.Request;
using EcoWatt.Model.DTO.Response;
using EcoWatt.Model;
using EcoWatt.Repository;
using EcoWatt.Service.ConvertMapping;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace EcoWatt.API.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar baterias.
    /// </summary>
    [Route("api/baterias/[controller]")]
    [ApiController]
    [Tags("Baterias")]
    public class BateriasController(IRepository<Bateria> bateriaRepository, IRepository<Usuario> usuarioRepository) : ControllerBase
    {
        private readonly IRepository<Bateria> _bateriaRepository = bateriaRepository;
        private readonly IRepository<Usuario> _usuarioRepository = usuarioRepository;
        private readonly BateriaService _bateriaService = new BateriaService();


        /// <summary>
        /// Retorna a lista completa de baterias.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     GET /bateria
        /// 
        /// Esse endpoint retorna uma lista de todos as baterias disponíveis no sistema.
        /// </remarks>
        /// <response code="200">Retorna a lista de baterias</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<BateriaResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<BateriaResponse>>> GetAll()
        {
            IEnumerable<Bateria> baterias = await _bateriaRepository.GetAll();

            //Retorna um enumerable de baterias sem os dados sensíveis.
            return Ok(_bateriaService.bateriasToResponseIEnumerable(baterias));

        }

        /// <summary>
        /// Retorna uma bateria com base no id fornecido.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     GET /bateria/id
        /// 
        /// Esse endpoint retorna uma bateria com base no id fornecido.
        /// </remarks>
        /// <response code="200">Retorna a bateria</response>
        /// <response code="404">Nenhuma bateria encontrada</response>
        /// <response code="500">Erro interno no servidor</response>
        // GET api/<BateriasController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BateriaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {

            var bateria = await _bateriaRepository.GetById(id);
            if (bateria == null)
            {
                return NotFound();
            }

            //Retorna a bateria sem seus dados sensíveis.
            return Ok(_bateriaService.bateriaToResponse(bateria));
        }

        /// <summary>
        /// Cria uma nova bateria.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     POST /bateria
        /// 
        /// Esse endpoint salva os dados da bateria no banco de dados.
        /// </remarks>
        /// <response code="200">Bateria criada</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Usuário não autorizado</response>
        /// <response code="404">Usuário não encontrado. </response>
        /// <response code="500">Erro de servidor</response>
        // POST api/<BateriasController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(BateriaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(BateriaRequest bateriaRequest)
        {
            Bateria bateria = new Bateria();
            BateriaResponse bateriaResponse = new BateriaResponse();

            bateria = _bateriaService.requestToBateria(bateriaRequest);

            if (bateria == null)
            {
                return BadRequest();
            }

            Usuario existingUsuario = await _usuarioRepository.GetById(bateria.IdUsuario);
            if (existingUsuario == null)
            {
                return NotFound("Usuário não cadastrado no sistema, por favor verificar id do usuário.");
            }

            await _bateriaRepository.Add(bateria);
            bateriaResponse = _bateriaService.bateriaToResponse(bateria);

            // Retorna a bateria sem seus dados sensíveis.
            return Ok(bateriaResponse);
        }

        /// <summary>
        /// Atualiza uma bateria com base no id fornecido.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     PUT /bateria/{id}
        /// 
        /// Esse endpoint atualiza a bateria com base no id fornecido.
        /// </remarks>
        /// <response code="200">Bateria atualizada</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Usuário não autorizado</response>
        /// <response code="404">Nenhuma bateria encontrada</response>
        /// <response code="500">Erro interno no servidor</response>
        // PUT api/<BateriasController>/5
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(BateriaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] BateriaRequest bateriaRequest)
        {

            Bateria bateria = new Bateria();
            BateriaResponse bateriaResponse = new BateriaResponse();

            bateria = _bateriaService.requestToBateria(bateriaRequest);
            bateria.IdBateria = id;

            Bateria existingBateria = await _bateriaRepository.GetById(id);

            if (existingBateria == null)
            {
                return NotFound();
            }

            Usuario existingUsuario = await _usuarioRepository.GetById(bateria.IdUsuario);
            if (existingUsuario == null)
            {
                return NotFound("Usuário não cadastrado no sistema, por favor verificar id do usuário.");
            }

            await _bateriaRepository.Update(id, bateria);
            bateriaResponse = _bateriaService.bateriaToResponse(bateria);

            // Retorna o bateria com seus dados alterados, sem seus dados sensíveis.
            return Ok(bateriaResponse);
        }

        /// <summary>
        /// Deleta uma bateria com base no id fornecido.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     DELETE/bateria/{id}
        /// 
        /// Esse endpoint deleta a bateria com base no id fornecido.
        /// </remarks>
        /// <response code="200">Bateria deletada</response>
        /// <response code="401">Usuário não autorizado</response>
        /// <response code="404">Nenhuma bateria encontrada</response>
        /// <response code="500">Erro interno no servidor</response>
        // DELETE api/<BateriasController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(EcoWatt.Model.ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {

            var bateria = await _bateriaRepository.GetById(id);
            if (bateria == null)
            {
                return NotFound();
            }

            await _bateriaRepository.Delete(id);

            //Retorna nada.
            return Ok();
        }

    }
}
