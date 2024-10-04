using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_ADMIN_NOTAS.Class
{
    public class EtiquetaRequest
    {
        public int IdEtiqueta { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdUsuario { get; set; }
    }
}
