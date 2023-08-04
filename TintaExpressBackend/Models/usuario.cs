using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class usuario
    {
        [Key]
        public int Id { get; set; }
        public string? nombre { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? telefono { get; set; }
        public string? direccion { get; set; }
        public int rol { get; set; }

    }
}
