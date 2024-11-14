using EcoWatt.Model;
using EcoWatt.Model.DTO.Request;
using EcoWatt.Model.DTO.Response;
using EcoWatt.Repository;
using EcoWatt.Service.Recommendation;
using EcoWatt.Service.UsuarioUseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcoWatt.API.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar recomendação de baterias.
    /// </summary>
    [Route("api/usuarioUses/[controller]")]
    [ApiController]
    [Tags("UsuarioUse")]
    public class BateriaRecommendationController(IUsuarioUseService usuarioUseService, RecommendationEngine recommendationEngine, IRepository<Bateria> repositoryBateria) : ControllerBase
    {
        private readonly IUsuarioUseService _usuarioUseService = usuarioUseService;
        private readonly IRepository<Bateria> _repositoryBateria = repositoryBateria;
        private readonly RecommendationEngine _recommendationEngine = recommendationEngine;

        /// <summary>
        /// Cria uma nova use de bateria.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     POST /usuarioUse
        /// 
        /// Esse endpoint salva uma nova use de bateria no banco de dados.
        /// </remarks>
        /// <response code="200">Use criada</response>
        /// <response code="401">Usuário não autorizado</response>
        /// <response code="500">Problema nas chaves estrangeiras.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> PostUsuarioUse([FromBody] UsuarioUseRequest usuarioUseRequest)
        {
            await _usuarioUseService.AddUsuarioUse(usuarioUseRequest);
            return Ok(usuarioUseRequest);
        }

        /// <summary>
        /// Retorna uma recomendação com base no id fornecido.
        /// </summary>
        /// <remarks>
        /// Exemplo de solicitação:
        /// 
        ///     GET /usuarioId
        /// 
        /// Esse endpoint retorna uma recomendação com base no id fornecido.
        /// </remarks>
        /// <response code="200">Retorna a recomendação</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Usuário não autorizado</response>
        /// <response code="500">Problema no carregamento de dados para a previsão.</response>
        [HttpGet("{usuarioId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<Bateria>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetRecommendations(int usuarioId)
        {
           
            var usuarioUses = await _usuarioUseService.GetUsuarioUses();

            _recommendationEngine.PrepareTrainModel(usuarioUses);

            var baterias = await _repositoryBateria.GetAll();

            var recommendedBaterias = new List<Bateria>();

            foreach (var bateria in baterias)
            {
                float score = _recommendationEngine.Predict(usuarioId, bateria.IdBateria);

                if (score > 0.5)
                {
                    recommendedBaterias.Add(bateria);
                }
            }

            return Ok(recommendedBaterias);
        }

    }
}
