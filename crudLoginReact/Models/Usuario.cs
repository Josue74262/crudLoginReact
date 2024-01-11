

using System.ComponentModel.DataAnnotations;

namespace crudLoginReact.Models
{
    public class Usuario
    {
        [Key]
        public int id_user { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? telefono { get; set; }
        public string? correo { get; set; }
        public string? contrasena { get; set; }
    }
}
