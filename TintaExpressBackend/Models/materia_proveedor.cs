using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class materia_proveedor
    {
        [Key]
        public int id { get; set; }
        public int id_materia { get; set; }
        public int id_proveedor { get; set; }
        public double costo { get; set; }
        public double cantidad_lote { get; set; }

    }
}
