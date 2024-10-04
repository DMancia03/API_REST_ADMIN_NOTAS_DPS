using API_REST_ADMIN_NOTAS.Class;
using API_REST_ADMIN_NOTAS.Data;
using API_REST_ADMIN_NOTAS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST_ADMIN_NOTAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordatoriosController : BasicController
    {
        public RecordatoriosController(AdminNotasContext db)
        {
            _db = db;
        }

        #region Get
        //Todos los recordatorios
        [HttpGet(Name = "GetRecordatorios")]
        public IActionResult Get()
        {
            List<RecordatorioRequest> notas = (from n in _db.Notas
                                               where n.FechaRecordatorio != null
                                               orderby n.FechaRecordatorio
                                               select new RecordatorioRequest
                                               {
                                                   IdNota = n.IdNota,
                                                   IdUsuario = n.IdUsuario,
                                                   IdEtiqueta = n.IdEtiqueta,
                                                   Nombre = n.Nombre,
                                                   Contenido = n.Contenido,
                                                   Estado = n.Estado,
                                                   FechaRecordatorio = n.FechaRecordatorio
                                               }).ToList();

            return Ok(notas);
        }

        //Un recordatorio
        [HttpGet("{IdRecordatorio}", Name = "GetRecordatorio")]
        public IActionResult GetRecordatorio(int IdRecordatorio)
        {
            var recordatorio = _db.Notas.Find(IdRecordatorio);

            if (recordatorio == null)
            {
                return NotFound(Helper.CrearError("No existe el recordatorio buscado"));
            }

            RecordatorioRequest recordatorioRequest = Helper.CrearRecordatorioRequest(recordatorio);
            return Ok(recordatorioRequest);
        }

        //Todos los recordatorios de un usuario
        [HttpGet("usuario/{IdUsuario}", Name = "GetRecordatoriosUsuario")]
        public IActionResult GetRecordatoriosUsuario(int IdUsuario)
        {
            List<RecordatorioRequest> notas = (from n in _db.Notas
                                       where n.FechaRecordatorio != null
                                       && n.Estado == Constantes.ESTADO_ACTIVO
                                       && n.IdUsuario == IdUsuario
                                       orderby n.FechaRecordatorio
                                       select new RecordatorioRequest
                                       {
                                           IdNota = n.IdNota,
                                           IdUsuario = n.IdUsuario,
                                           IdEtiqueta = n.IdEtiqueta,
                                           Nombre = n.Nombre,
                                           Contenido = n.Contenido,
                                           Estado = n.Estado,
                                           FechaRecordatorio = n.FechaRecordatorio
                                       }).ToList();

            return Ok(notas);
        }

        //Todos los recordatorios de un usuario y por etiqueta
        [HttpGet("usuario/{IdUsuario}/etiqueta/{NombreEtiqueta}", Name = "GetRecordatoriosUsuarioEtiqueta")]
        public IActionResult GetRecordatoriosUsuarioEtiqueta(int IdUsuario, string NombreEtiqueta)
        {
            List<NotaRequest> notas = (from n in _db.Notas
                                       join e in _db.Etiquetas on n.IdEtiqueta equals e.IdEtiqueta
                                       where n.FechaRecordatorio != null
                                       && n.Estado == Constantes.ESTADO_ACTIVO
                                       && EF.Functions.Like(e.Nombre, NombreEtiqueta)
                                       && n.IdUsuario == IdUsuario
                                       orderby n.FechaRecordatorio
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
    }
}
