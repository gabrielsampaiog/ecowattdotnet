using EcoWatt.Model.DTO.Request;
using EcoWatt.Model.DTO.Response;
using EcoWatt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoWatt.Service.ConvertMapping
{
    public class BateriaService
    {
        public Bateria requestToBateria(BateriaRequest bateriaRequest)
        {
            Bateria bateria = new Bateria();

            bateria.IdUsuario = bateriaRequest.IdUsuario;
            bateria.DsTipoBateria = bateriaRequest.DsTipoBateria;
            bateria.DsNomeConjunto = bateriaRequest.DsNomeConjunto;
            bateria.VlCapacidade = bateriaRequest.VlCapacidade;
            bateria.VlQuantidade = bateriaRequest.VlQuantidade;
            bateria.DsStatus = bateriaRequest.DsStatus;
            bateria.DtUltimaLeitura = bateriaRequest.DtUltimaLeitura;
            bateria.DtCriacao = bateriaRequest.DtCriacao;
            bateria.DtModificacao = bateriaRequest.DtModificacao;
            
            return bateria;
        }

        public BateriaResponse bateriaToResponse(Bateria bateria)
        {
            BateriaResponse bateriaResponse = new BateriaResponse();

            bateriaResponse.IdBateria = bateria.IdBateria;
            bateriaResponse.IdUsuario = bateria.IdUsuario;
            bateriaResponse.DsTipoBateria = bateria.DsTipoBateria;
            bateriaResponse.DsNomeConjunto = bateria.DsNomeConjunto;
            bateriaResponse.VlCapacidade = bateria.VlCapacidade;
            bateriaResponse.VlQuantidade = bateria.VlQuantidade;
            bateriaResponse.DsStatus = bateria.DsStatus;
            bateriaResponse.DtCriacao = bateria.DtCriacao;
            bateriaResponse.DtModificacao = bateria.DtModificacao;

            return bateriaResponse;
        }

        public IEnumerable<BateriaResponse> bateriasToResponseIEnumerable(IEnumerable<Bateria> baterias)
        {
            var bateriaResponses = baterias.Select(bateria => new BateriaResponse
            {
                IdBateria = bateria.IdBateria,
                IdUsuario = bateria.IdUsuario,
                DsTipoBateria = bateria.DsTipoBateria,
                DsNomeConjunto = bateria.DsNomeConjunto,
                VlCapacidade = bateria.VlCapacidade,
                VlQuantidade = bateria.VlQuantidade,
                DsStatus = bateria.DsStatus,
                DtCriacao = bateria.DtCriacao,
                DtModificacao = bateria.DtModificacao
            });

            return bateriaResponses;
        }

    }
}
