using API_REST_ADMIN_NOTAS.Data;
using API_REST_ADMIN_NOTAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_ADMIN_NOTAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        protected AdminNotasContext _db;

        public UsuariosController(AdminNotasContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetUsuarios")]
        public IActionResult Get()
        {
            return Ok(_db.Usuarios.ToList());
        }

        //[HttpGet(Name = "GetUsuarios/{id}")]
        //public IActionResult GetOne(int id)
        //{
        //    var usuario = _db.Usuarios.Find(id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(usuario);
        //}

        [HttpPost(Name = "CreateUsuario")]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();
            //return CreatedAtRoute("GetUsuarios", new { id = usuario.IdUsuario }, usuario);
            return Ok(usuario);
        }
    }
}
