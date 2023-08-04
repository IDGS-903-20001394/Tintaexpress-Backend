using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class materia_prima
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public float inventario { get; set; }
        public string? unidad { get; set; }
        public float minimo { get; set; }
    }
}
