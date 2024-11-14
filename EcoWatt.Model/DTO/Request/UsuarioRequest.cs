using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoWatt.Model.DTO.Request
{
    public class UsuarioRequest
    {

        [MinLength(20, ErrorMessage = "O nome de usuário deve conter no minimo 20 caracteres.")]
        [MaxLength(150, ErrorMessage = "O nome de usuário deve conter no máximo 150 caracteres.")]
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string DsUsuario { get; set; }

        [MinLength(20, ErrorMessage = "A senha deve conter no minimo 20 caracteres.")]
        [MaxLength(255, ErrorMessage = "A senha deve conter no máximo 255 caracteres.")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string DsSenha { get; set; }

        [MinLength(20, ErrorMessage = "O nome completo do usuário deve conter no minimo 20 caracteres.")]
        [MaxLength(250, ErrorMessage = "O nome completo do usuário deve conter no máximo 250 caracteres.")]
        [Required(ErrorMessage = "O nome completo do usuário é obrigatório.")]
        public string DsNomeCompleto { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }

    }
}
