using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_VA_650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_VA_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogDBContext _blogDBContexto;

        public comentariosController(blogDBContext blogDBContexto)
        {
            _blogDBContexto = blogDBContexto;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listadoComentarios = (from e in _blogDBContexto.comentarios
                                                          select e).ToList();
            if (listadoComentarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoComentarios);
        }

        [HttpPost]
        [Route("Agregar")]

        public IActionResult PostCalificacion([FromBody] comentarios comentario)
        {
            try
            {
                _blogDBContexto.comentarios.Add(comentario);
                _blogDBContexto.SaveChanges();

                return Ok("Comentario agregado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarComentario(int id, [FromBody] comentarios comentarioActualizado)
        {
            var comentario = _blogDBContexto.comentarios.FirstOrDefault(c => c.ComentarioId == id);
            if (comentario == null)
            {
                return NotFound($"Comentario con ID {id} no encontrado");
            }

            comentario.PublicacionId = comentarioActualizado.PublicacionId;
            comentario.UsuarioId = comentarioActualizado.UsuarioId;
            comentario.comentario = comentarioActualizado.comentario;

            _blogDBContexto.SaveChanges();

            return Ok($"Comentario con ID {id} actualizado exitosamente");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarComentario(int id)
        {
            var comentario = _blogDBContexto.comentarios.FirstOrDefault(c => c.ComentarioId == id);
            if (comentario == null)
            {
                return NotFound($"Calificación con ID {id} no encontrada");
            }

            _blogDBContexto.comentarios.Remove(comentario);
            _blogDBContexto.SaveChanges();

            return Ok($"Calificación con ID {id} eliminada exitosamente");
        }

        [HttpGet]
        [Route("ComentariosPorUsuario/{usuarioId}")]
        public IActionResult ComentariosPorUsuario(int usuarioId)
        {
            var comentarios = _blogDBContexto.comentarios.Where(c => c.UsuarioId == usuarioId).ToList();
            if (comentarios.Count == 0)
            {
                return NotFound($"No se encontraron comentarios para el usuario con ID {usuarioId}");
            }

            return Ok(comentarios);
        }

    }
}
