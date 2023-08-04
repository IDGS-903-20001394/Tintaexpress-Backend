using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class categoria
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
    }
}
