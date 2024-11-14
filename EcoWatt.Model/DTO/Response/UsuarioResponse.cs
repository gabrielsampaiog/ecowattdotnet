

namespace EcoWatt.Model.DTO.Response
{
    public class UsuarioResponse
    {

        //DTO criado excluindo a senha por conta da sensibilidade dos dados.

        public int IdUsuario { get; set; }

        public string DsUsuario { get; set; }

        public string DsNomeCompleto { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}
