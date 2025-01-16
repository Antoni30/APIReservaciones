using Microsoft.EntityFrameworkCore;

namespace GestionProyectos
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relación entre Producto y Categoria
            modelBuilder.Entity<Producto>()
                .HasOne<Categoria>() // Indicar que cada producto tiene una categoría
                .WithMany() // No es necesario tener relación inversa en Productos
                .HasForeignKey(p => p.CategoriaId); // La clave foránea en Producto

            modelBuilder.Entity<Producto>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd(); // Para hacer que el ID de Producto sea autoincrementable

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd(); // Para hacer que el ID de Categoria sea autoincrementable

            // Configuración de la clave primaria de Producto
            modelBuilder.Entity<Producto>()
                .HasKey(p => p.Id);

            // Configuración de la clave primaria de Categoria
            modelBuilder.Entity<Categoria>()
                .HasKey(c => c.Id);
        }
    }

    public class Producto
    {
        public required int Id { get; set; } // No nullable
        public required int CategoriaId { get; set; } // Clave foránea
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; } // Puede ser null
    }

    public class Categoria
    {
        public required int Id { get; set; } // No nullable
        public  required string Nombre { get; set; }
    }
}
