using Microsoft.EntityFrameworkCore;

namespace GestionProyectos
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set;} 
    }

    public class Producto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
