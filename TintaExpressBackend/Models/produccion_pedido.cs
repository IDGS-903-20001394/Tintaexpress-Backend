using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class produccion_pedido
    {
        [Key]
        public int id { get; set; }
        public int id_pedido { get; set; }
        public string? estatus { get; set; }
    }
}
