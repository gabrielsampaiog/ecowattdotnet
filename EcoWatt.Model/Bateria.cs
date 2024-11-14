using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoWatt.Model
{
    [Table("ecowatt_baterias")]
    public class Bateria
    {

        [Key]
        [Column("id_bateria")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBateria { get; set; }

        [Column("id_usuario", TypeName = "number(11)")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [Column("ds_tipo_bateria", TypeName = "varchar(50)")]
        public string DsTipoBateria { get; set; }

        [Column("ds_nome_conjunto", TypeName = "varchar(100)")]
        public string DsNomeConjunto { get; set; }

        [Column("vl_capacidade", TypeName = "number(11)")]
        public long VlCapacidade { get; set; }

        [Column("vl_quantidade", TypeName = "number(11)")]
        public long VlQuantidade { get; set; }

        [Column("ds_status", TypeName = "varchar(50)")]
        public string DsStatus { get; set; }

        [Column("dt_timestamp_ultima_leitura", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtUltimaLeitura { get; set; }

        [Column("dt_criacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao { get; set; }

        [Column("dt_modificacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }
    }
}
