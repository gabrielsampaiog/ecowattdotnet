using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoWatt.Model.DTO.Response
{
    public class BateriaResponse
    {

        //DTO criado excluindo a data de ultima leitura por conta da sensibilidade dos dados.

        public int IdBateria { get; set; }

        public int IdUsuario { get; set; }

        public string DsTipoBateria { get; set; }

        public string DsNomeConjunto { get; set; }

        public long VlCapacidade { get; set; }

        public long VlQuantidade { get; set; }

        public string DsStatus { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}
