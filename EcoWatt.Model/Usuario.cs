using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoWatt.Model
{
    [Table("ecowatt_usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Column("ds_usuario", TypeName = "varchar(150)")]
        public string DsUsuario { get; set; }

        [Column("ds_senha", TypeName = "varchar(225)")]
        public string DsSenha { get; set; }


        [Column("ds_nome_completo", TypeName = "varchar(250)")]
        public string DsNomeCompleto { get; set; }


        [Column("dt_criacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao {  get; set; }


        [Column("dt_modificacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }

        public ICollection<Bateria> Baterias { get; set; } = new List<Bateria>();


    }
}
