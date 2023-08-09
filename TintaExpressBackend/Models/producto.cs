using System.ComponentModel.DataAnnotations;

namespace TintaExpressBackend.Models
{
    public class producto
    {
        [Key]
        public int id { get; set; }
        public string? Nombre { get; set; }
        public double Precio { get; set; }
        public string? Descripcion { get; set;}
        public string? Imagen { get; set;}
        public int Estatus { get; set;}
        public int materia_base { get; set; }
        public int id_categoria { get; set; }

    }
}
