using Microsoft.EntityFrameworkCore;
using TintaExpressBackend.Models;

namespace TintaExpressBackend.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options) { }

        public DbSet<producto> producto { get; set; }
        public DbSet<categoria> categoria { get; set; }
        public DbSet<materia_prima> materia_prima { get; set; }
        public DbSet<materia_producto> materia_producto { get; set; }
        public DbSet<proveedor> proveedor { get; set;}
        public DbSet<materia_proveedor> materia_proveedor { get; set; }
        public DbSet<provision> provision { get; set; }
        public DbSet<carrito> carrito { get; set; }
        public DbSet<usuario> usuario { get; set; }
        public DbSet<rol> rol { get; set; }
        public DbSet<producto_pedido> producto_pedido { get; set; }
        public DbSet<pedido> pedido { get; set; }
        public DbSet<produccion_pedido> produccion_pedido { get; set; }

    }
}
