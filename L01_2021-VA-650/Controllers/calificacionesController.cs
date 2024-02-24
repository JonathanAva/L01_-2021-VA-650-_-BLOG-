using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_VA_650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_VA_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {

        private readonly blogDBContext _blogDBContexto;

        public calificacionesController(blogDBContext blogDBContexto)
        {
            _blogDBContexto = blogDBContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<calificaciones> listadoCalificaciones = (from e in _blogDBContexto.calificaciones
                                                          select e).ToList();
            if (listadoCalificaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoCalificaciones);
        }

        [HttpPost]
        [Route("Agregar")]

        public IActionResult PostCalificacion([FromBody] calificaciones calificacion)
        {
            try
            {
                _blogDBContexto.calificaciones.Add(calificacion);
                _blogDBContexto.SaveChanges();

                return Ok("Calificación agregada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarCalificacion(int id, [FromBody] calificaciones calificacionActualizada)
        {
            var calificacion = _blogDBContexto.calificaciones.FirstOrDefault(c => c.CalificacionId == id);
            if (calificacion == null)
            {
                return NotFound($"Calificación con ID {id} no encontrada");
            }

            calificacion.PublicacionId = calificacionActualizada.PublicacionId;
            calificacion.UsuarioId = calificacionActualizada.UsuarioId;
            calificacion.calificacion = calificacionActualizada.calificacion;

            _blogDBContexto.SaveChanges();

            return Ok($"Calificación con ID {id} actualizada exitosamente");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarCalificacion(int id)
        {
            var calificacion = _blogDBContexto.calificaciones.FirstOrDefault(c => c.CalificacionId == id);
            if (calificacion == null)
            {
                return NotFound($"Calificación con ID {id} no encontrada");
            }

            _blogDBContexto.calificaciones.Remove(calificacion);
            _blogDBContexto.SaveChanges();

            return Ok($"Calificación con ID {id} eliminada exitosamente");
        }

        [HttpGet]
        [Route("CalificacionesPorPublicacion/{publicacionId}")]
        public IActionResult CalificacionesPorPublicacion(int publicacionId)
        {
            var calificaciones = _blogDBContexto.calificaciones.Where(c => c.PublicacionId == publicacionId).ToList();
            if (calificaciones.Count == 0)
            {
                return NotFound($"No se encontraron calificaciones para la publicación con ID {publicacionId}");
            }

            return Ok(calificaciones);
        }



    }
}
