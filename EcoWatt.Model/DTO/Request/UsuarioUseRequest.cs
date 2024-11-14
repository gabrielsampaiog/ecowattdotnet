
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace EcoWatt.Model.DTO.Request
{
    public class UsuarioUseRequest
    {

        [Required(ErrorMessage = "O id do usuário é obrigatório.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O id da bateria é obrigatório.")]
        public int IdBateria { get; set; }

        [NotMapped]
        public DateTime UsedAt { get; set; } = DateTime.Now;
    }
}
