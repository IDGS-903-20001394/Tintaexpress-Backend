using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class materia_producto
    {
        [Key]
        public int id { get; set; }
        public int id_producto { get; set; }
        public int id_materia { get; set; }
        public double cantidad { get; set; }

    }
}
