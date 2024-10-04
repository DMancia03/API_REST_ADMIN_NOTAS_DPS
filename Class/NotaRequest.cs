using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_ADMIN_NOTAS.Class
{
    public class NotaRequest
    {
        public int IdNota { get; set; }
        public string Contenido { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdEtiqueta { get; set; }
        public int IdUsuario { get; set; }
    }
}
