using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class rol
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
    }
}