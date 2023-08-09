using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class carrito
    {
        [Key]
        public int id { get; set; }
        public int id_producto { get; set; }
        public int id_usuario { get; set; }
        public int cantidad { get; set; }
        public double total { get; set; }
        public string? imagen { get; set; }
    }
}
