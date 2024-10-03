using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_ADMIN_NOTAS.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("USERNAME", TypeName = "NVARCHAR(50)")]
        public string Username { get; set; } = null!;

        [Column("PASS", TypeName = "NVARCHAR(100)")]
        public string Pass { get; set; } = null!;
    }
}
