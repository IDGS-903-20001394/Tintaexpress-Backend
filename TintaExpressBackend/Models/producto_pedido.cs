using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class producto_pedido
    {
        [Key]
        public int id { get; set; }
        public int id_producto { get; set; }
        public int id_pedido { get; set; }
        public int cantidad { get; set; }
        public float total { get; set; }
        public string? imagen { get; set; }
    }
}
