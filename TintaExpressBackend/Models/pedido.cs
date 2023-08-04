using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class pedido
    {
        [Key]
        public int id { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha { get; set; }
        public string? direccion { get; set; }
        public float total { get; set; }
        public string? estatus { get; set; }
    }
}
