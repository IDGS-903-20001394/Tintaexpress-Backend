using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class pedido
    {
        [Key]
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string? fecha { get; set; }
        public string? direccion { get; set; }
        public double total { get; set; }
        public string? estatus { get; set; }
    }
}
