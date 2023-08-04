using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class materia_proveedor
    {
        [Key]
        public int id { get; set; }
        public int id_materia { get; set; }
        public int id_proveedor { get; set; }
        public float costo { get; set; }
        public float cantidad_lote { get; set; }

    }
}
