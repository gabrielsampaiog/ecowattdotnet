
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EcoWatt.Model
{
    [Table("ecowatt_usuario_use")]
    public class UsuarioUse
    {
        [Key]
        [Column("id_use")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUse { get; set; }

        [Column("id_usuario", TypeName = "number(11)")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [Column("id_bateria", TypeName = "number(11)")]
        public int IdBateria { get; set; }

        [ForeignKey("IdBateria")]
        public Bateria Bateria { get; set; }

        public DateTime UsedAt { get; set; } = DateTime.Now;
    }
}
