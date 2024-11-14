using System.ComponentModel.DataAnnotations;


namespace EcoWatt.Model.DTO.Request
{
    public class BateriaRequest
    {

        [Required(ErrorMessage = "O id do usuário é obrigatório.")]
        public int IdUsuario { get; set; }

        [MinLength(20, ErrorMessage = "A descrição do tipo da bateria deve conter no minimo 20 caracteres.")]
        [MaxLength(50, ErrorMessage = "A descrição do tipo da bateria deve conter no máximo 50 caracteres.")]
        [Required(ErrorMessage = "A descrição do tipo da bateria é obrigatória.")]
        public string DsTipoBateria { get; set; }

        [MinLength(20, ErrorMessage = "A descrição do nome do conjunto deve conter no minimo 20 caracteres.")]
        [MaxLength(100, ErrorMessage = "A descrição do nome do conjunto deve conter no máximo 100 caracteres.")]
        [Required(ErrorMessage = "A descrição do nome do conjunto é obrigatória.")]
        public string DsNomeConjunto { get; set; }

        [Range(1, 99999999999, ErrorMessage = "O valor deve estar entre 1 e 99999999999.")]
        [Required(ErrorMessage = "O valor da capacidade da bateria é obrigatório.")]
        public long VlCapacidade { get; set; }

        [Range(1, 99999999999, ErrorMessage = "O valor deve estar entre 1 e 99999999999.")]
        [Required(ErrorMessage = "O valor da quantidade da bateria é obrigatório.")]
        public long VlQuantidade { get; set; }

        [MinLength(20, ErrorMessage = "A descrição do status deve conter no minimo 20 caracteres.")]
        [MaxLength(50, ErrorMessage = "A descrição do status deve conter no máximo 50 caracteres.")]
        [Required(ErrorMessage = "A descrição do status é obrigatória.")]
        public string DsStatus { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DtUltimaLeitura { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime DtCriacao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }
    }
}
