using API_REST_ADMIN_NOTAS.Class;
using API_REST_ADMIN_NOTAS.Data;
using API_REST_ADMIN_NOTAS.Helpers;
using API_REST_ADMIN_NOTAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_ADMIN_NOTAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : BasicController
    {
        public UsuariosController(AdminNotasContext db)
        {
            _db = db;
        }

        [HttpPost("signup", Name = "SignUpUsuario")]
        public IActionResult Register([FromBody] UserRequest user)
        {
            //Validar si existe el username en bd
            int username_count = _db.Usuarios.Where(u => u.Username == user.Username).Count();
            
            //Si existe se devuelve un error y se corta el flujo
            if(username_count > 0)
            {
                return BadRequest(Helper.CrearError("Nombre de usuario ya existe"));
            }

            //Crear usuario
            Usuario usuario = Helper.NormalizarUsuario(user);

            //Guardar usuario en bd
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();

            //Devolver objeto de session personalizado
            return Ok(Helper.CrearSesion(usuario));
        }

        [HttpPost("login", Name = "LoginUsuario")]
        public IActionResult Login([FromBody] UserRequest user)
        {
            //Buscamos el usuario en bd
            var usuario = _db.Usuarios.Where(u => u.Username == user.Username && u.Pass == user.Pass).FirstOrDefault();

            //Si no existe retornamos un error
            if(usuario == null)
            {
                return BadRequest(Helper.CrearError("Login fallo"));
            }

            //Devolver objeto de session personalizado
            return Ok(Helper.CrearSesion(usuario));
        }

    }
}
