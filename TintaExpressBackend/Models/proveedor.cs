using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class proveedor
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string empresa { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public int estatus { get; set; }
    }
}
