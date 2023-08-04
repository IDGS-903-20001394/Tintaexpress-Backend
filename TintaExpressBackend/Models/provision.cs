using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class provision
    {
        [Key]
        public int id { get; set; }
        public int id_proveedor { get; set; }
        public int id_materia { get; set; }
        public float cantidad { get; set; }
        public float costoTotal { get; set; }
        public DateTime fechaPedido { get; set; }
        public DateTime fechaEntrega { get; set; }
        public string estatus { get; set; }
    }
}
