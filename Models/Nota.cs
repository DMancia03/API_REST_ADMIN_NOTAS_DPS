using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_ADMIN_NOTAS.Models
{
    [Table("NOTA")]
    public class Nota
    {
        [Key]
        [Column("ID_NOTA")]
        public int IdNota { get; set; }

        [Column("CONTENIDO", TypeName = "NVARCHAR(200)")]
        public string Contenido { get; set; } = null!;

        [Column("ESTADO", TypeName = "NVARCHAR(10)")]
        public string Estado { get; set; } = null!;

        [Column("NOMBRE", TypeName = "NVARCHAR(50)")]
        public string Nombre { get; set; } = null!;

        [Column("FECHA_RECORDATORIO", TypeName = "DATETIME")]
        public DateTime FechaRecordatorio { get; set; }

        [Column("ID_ETIQUETA")]
        public int IdEtiqueta { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        public Etiqueta? Etiqueta { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
