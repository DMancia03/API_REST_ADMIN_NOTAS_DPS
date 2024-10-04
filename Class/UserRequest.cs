using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_ADMIN_NOTAS.Class
{
    public class UserRequest
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; } = null!;
        public string Pass { get; set; } = null!;
    }
}
