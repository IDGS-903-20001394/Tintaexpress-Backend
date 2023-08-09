using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class provision
    {
        [Key]
        public int id { get; set; }
        public int id_proveedor { get; set; }
        public int id_materia { get; set; }
        public double cantidad { get; set; }
        public double costoTotal { get; set; }
        public DateTime fechaPedido { get; set; }
        public DateTime fechaEntrega { get; set; }
        public string estatus { get; set; }
    }
}
