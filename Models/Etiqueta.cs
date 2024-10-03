using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_ADMIN_NOTAS.Models
{
    [Table("ETIQUETA")]
    public class Etiqueta
    {
        [Key]
        [Column("ID_ETIQUETA")]
        public int IdEtiqueta { get; set; }

        [Column("NOMBRE", TypeName = "NVARCHAR(50)")]
        public string Nombre { get; set; } = null!;

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }
        public ICollection<Nota>? Notas { get; set; }
    }
}
