using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_VA_650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_VA_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogDBContext _blogDB;

        public usuariosController(blogDBContext blogDBContexto)
        { 
            _blogDB = blogDBContexto;   
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listadoUsuarios = (from e in _blogDB.usuarios
                                           select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }


        
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuario([FromBody] usuarios usuario)
        {
            try
            {
                _blogDB.usuarios.Add(usuario);
                _blogDB.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] usuarios usuarioActualizado)
        {
            var usuario = _blogDB.usuarios.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }

            usuario.RolId = usuarioActualizado.RolId;
            usuario.NombreUsuario = usuarioActualizado.NombreUsuario;
            usuario.Clave = usuarioActualizado.Clave;
            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Apellido = usuarioActualizado.Apellido;

            _blogDB.SaveChanges();

            return Ok($"Usuario con ID {id} actualizado exitosamente");
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _blogDB.usuarios.FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }

            _blogDB.usuarios.Remove(usuario);
            _blogDB.SaveChanges();

            return Ok($"Usuario con ID {id} eliminado exitosamente");
        }

        [HttpGet]
        [Route("FiltroNombreyApellido")]
        public IActionResult BuscarPorNombreApellido(string nombre, string apellido)
        {
            var usuariosFiltrados = _blogDB.usuarios
                                    .Where(u => u.Nombre.Contains(nombre) && u.Apellido.Contains(apellido))
                                    .ToList();

            if (usuariosFiltrados.Count == 0)
            {
                return NotFound("No se encontraron usuarios con el nombre y apellido ingresados");
            }

            return Ok(usuariosFiltrados);
        }


        [HttpGet]
        [Route("BuscarPorRol")]
        public IActionResult BuscarPorRol(int rolId)
        {
            var usuariosFiltrados = _blogDB.usuarios
                .Where(u => u.RolId == rolId)
                .ToList();

            if (usuariosFiltrados.Count == 0)
            {
                return NotFound("No se encontraron usuarios con el rol ingresado");
            }

            return Ok(usuariosFiltrados);
        }


    }
}
