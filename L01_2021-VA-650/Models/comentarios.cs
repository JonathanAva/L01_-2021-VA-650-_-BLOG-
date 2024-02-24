using System.ComponentModel.DataAnnotations;

namespace L01_2021_VA_650.Models
{
    public class comentarios
    {
        [Key]
        public int ComentarioId { get; set; }
        public int PublicacionId { get; set; }
        public string? comentario { get; set; }
        public int UsuarioId { get; set; }
    }
}
