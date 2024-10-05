using API_REST_ADMIN_NOTAS.Class;
using API_REST_ADMIN_NOTAS.Data;
using API_REST_ADMIN_NOTAS.Helpers;
using API_REST_ADMIN_NOTAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST_ADMIN_NOTAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotasController : BasicController
    {
        public NotasController(AdminNotasContext db)
        {
            _db = db;
        }

        #region Get
        //Todas las notas
        [HttpGet(Name = "GetNotas")]
        public IActionResult Get()
        {
            List<NotaRequest> notas = (from n in _db.Notas
                                       where n.FechaRecordatorio == null
                                       select new NotaRequest
                                       {
                                           IdNota = n.IdNota,
                                           IdUsuario = n.IdUsuario,
                                           IdEtiqueta = n.IdEtiqueta,
                                           Nombre = n.Nombre,
                                           Contenido = n.Contenido,
                                           Estado = n.Estado
                                       }).ToList();

            return Ok(notas);
        }

        //Una unica nota
        [HttpGet("{IdNota}", Name = "GetNota")]
        public IActionResult GetNota(int IdNota)
        {
            var nota = _db.Notas.Find(IdNota);

            if(nota == null)
            {
                return NotFound(Helper.CrearError("No existe la nota buscada"));
            }

            NotaRequest notaRequest = Helper.CrearNotaRequest(nota);
            return Ok(notaRequest);
        }

        //Por id usuario
        [HttpGet("usuario/{IdUsuario}", Name = "GetNotaUsuario")]
        public IActionResult GetNotaUsuario(int IdUsuario)
        {
            List<NotaRequest> notas = (from n in _db.Notas
                                       where n.FechaRecordatorio == null
                                       && n.Estado == Constantes.ESTADO_ACTIVO
                                       && n.IdUsuario == IdUsuario
                                       select new NotaRequest
                                       {
                                           IdNota = n.IdNota,
                                           IdUsuario = n.IdUsuario,
                                           IdEtiqueta = n.IdEtiqueta,
                                           Nombre = n.Nombre,
                                           Contenido = n.Contenido,
                                           Estado = n.Estado
                                       }).ToList();

            return Ok(notas);
        }

        //Por id usuario e id etiqueta
        [HttpGet("usuario/{IdUsuario}/id_etiqueta/{IdEtiqueta}", Name = "GetNotaIdEtiqueta")]
        public IActionResult GetNotaEtiqueta(int IdUsuario, int IdEtiqueta)
        {
            List<NotaRequest> notas = (from n in _db.Notas
                                       where n.FechaRecordatorio == null
                                       && n.Estado == Constantes.ESTADO_ACTIVO
                                       && n.IdEtiqueta == IdEtiqueta
                                       && n.IdUsuario == IdUsuario
                                       select new NotaRequest
                                       {
                                           IdNota = n.IdNota,
                                           IdUsuario = n.IdUsuario,
                                           IdEtiqueta = n.IdEtiqueta,
                                           Nombre = n.Nombre,
                                           Contenido = n.Contenido,
                                           Estado = n.Estado
                                       }).ToList();

            return Ok(notas);
        }

        //Por id usuario y nombre etiqueta
        [HttpGet("usuario/{IdUsuario}/nombre_etiqueta/{NombreEtiqueta}", Name = "GetNotaNombreEtiqueta")]
        public IActionResult GetNotaEtiqueta(int IdUsuario,string NombreEtiqueta)
        {
            NombreEtiqueta  = "%" + NombreEtiqueta + "%";
            List<NotaRequest> notas = (from n in _db.Notas
                                       join e in _db.Etiquetas on n.IdEtiqueta equals e.IdEtiqueta
                                       where n.FechaRecordatorio == null
                                       && n.Estado == Constantes.ESTADO_ACTIVO
                                       && EF.Functions.Like(e.Nombre, NombreEtiqueta)
                                       && n.IdUsuario == IdUsuario
                                       select new NotaRequest
                                       {
                                           IdNota = n.IdNota,
                                           IdUsuario = n.IdUsuario,
                                           IdEtiqueta = n.IdEtiqueta,
                                           Nombre = n.Nombre,
                                           Contenido = n.Contenido,
                                           Estado = n.Estado
                                       }).ToList();

            return Ok(notas);
        }

        //Por id usuario y que esten en papelera
        [HttpGet("usuario/{IdUsuario}/papelera", Name = "GetNotasPapelera")]
        public IActionResult GetNotasPapelera(int IdUsuario)
        {
            List<NotaRequest> notas = (from n in _db.Notas
                                       where n.FechaRecordatorio == null
                                       && n.Estado == Constantes.ESTADO_EN_PAPELERA
                                       && n.IdUsuario == IdUsuario
                                       select new NotaRequest
                                       {
                                           IdNota = n.IdNota,
                                           IdUsuario = n.IdUsuario,
                                           IdEtiqueta = n.IdEtiqueta,
                                           Nombre = n.Nombre,
                                           Contenido = n.Contenido,
                                           Estado = n.Estado
                                       }).ToList();

            return Ok(notas);
        }
        #endregion

        #region Post
        //Crear nota
        [HttpPost(Name = "CrearNota")]
        public IActionResult CrearNota([FromBody] NotaRequest notaRequest)
        {
            notaRequest.Estado = Constantes.ESTADO_ACTIVO;

            Nota nota = Helper.NormalizarNota(notaRequest);

            _db.Notas.Add(nota);
            _db.SaveChanges();

            notaRequest = Helper.CrearNotaRequest(nota);

            return Ok(notaRequest);
        }
        #endregion

        #region Put
        //Actualizar nota
        [HttpPut("{IdNota}", Name = "ActualizarNota")]
        public IActionResult ActualizarNota(int IdNota, [FromBody] NotaRequest notaRequest)
        {
            var nota = _db.Notas.Find(IdNota);

            if(nota == null)
            {
                return NotFound(Helper.CrearError("No existe la nota que se quiere actualizar"));
            }

            nota.Nombre = notaRequest.Nombre;
            nota.Contenido = notaRequest.Contenido;
            nota.IdEtiqueta = notaRequest.IdEtiqueta;

            _db.Notas.Update(nota);
            _db.SaveChanges();

            notaRequest = Helper.CrearNotaRequest(nota);

            return Ok(notaRequest);
        }

        //Mover nota a papelera
        [HttpPut("papelera/{IdNota}", Name = "MoverNotaPapelera")]
        public IActionResult MoverNotaPapelera(int IdNota, [FromBody] NotaRequest notaRequest)
        {
            var nota = _db.Notas.Find(IdNota);

            if (nota == null)
            {
                return NotFound(Helper.CrearError("No existe la nota que se quiere mover a la papelera"));
            }

            nota.Estado = Constantes.ESTADO_EN_PAPELERA;

            _db.Notas.Update(nota);
            _db.SaveChanges();

            notaRequest = Helper.CrearNotaRequest(nota);

            return Ok(notaRequest);
        }

        //Restaurar nota de papelera
        [HttpPut("papelera/restaurar/{IdNota}", Name = "RestaurarNotaPapelera")]
        public IActionResult RestaurarNotaPapelera(int IdNota, [FromBody] NotaRequest notaRequest)
        {
            var nota = _db.Notas.Find(IdNota);

            if (nota == null)
            {
                return NotFound(Helper.CrearError("No existe la nota que se quiere restaurar"));
            }

            nota.Estado = Constantes.ESTADO_ACTIVO;

            _db.Notas.Update(nota);
            _db.SaveChanges();

            notaRequest = Helper.CrearNotaRequest(nota);

            return Ok(notaRequest);
        }
        #endregion

        #region Delete
        //Borrar definitivamente nota
        [HttpDelete("{IdNota}", Name = "BorrarNota")]
        public IActionResult BorrarNota(int IdNota)
        {
            var nota = _db.Notas.Find(IdNota);

            if (nota == null)
            {
                return BadRequest(Helper.CrearError("No existe la nota que se quiere borrar"));
            }

            _db.Notas.Remove(nota);
            _db.SaveChanges();

            NotaRequest notaRequest = Helper.CrearNotaRequest(nota);
            return Ok(notaRequest);
        }
        #endregion
    }
}
