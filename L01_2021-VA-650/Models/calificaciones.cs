using System.ComponentModel.DataAnnotations;

namespace L01_2021_VA_650.Models
{
    public class calificaciones
    {
        [Key]
        public int CalificacionId { get; set; }
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public int calificacion { get; set; }
    }
}
