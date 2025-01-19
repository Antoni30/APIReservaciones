using Microsoft.EntityFrameworkCore;

namespace GestionProyectos
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Habitaciones> Habitaciones { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<ServiciosAdicionales> ServiciosAdicionales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(c => c.IdCliente);
                entity.Property(c => c.IdCliente)
                      .ValueGeneratedOnAdd();
                entity.Property(c => c.nombre)
                      .IsRequired();
            });

            modelBuilder.Entity<Habitaciones>(entity =>
            {
                entity.HasKey(h => h.IdHabitacion);
                entity.Property(h => h.IdHabitacion)
                      .ValueGeneratedOnAdd();
                entity.Property(h => h.numero)
                      .IsRequired();
            });

            modelBuilder.Entity<Reservas>(entity =>
            {
                entity.HasKey(r => r.IdRecerva);
                entity.Property(r => r.IdRecerva)
                      .ValueGeneratedOnAdd(); 

                entity.HasOne<Habitaciones>()
                      .WithMany()
                      .HasForeignKey(r => r.IdHabitacionFK) 
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Clientes>()
                      .WithMany()
                      .HasForeignKey(r => r.IdClienteFK)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ServiciosAdicionales>(entity =>
            {
                entity.HasKey(s => s.IdServicio);
                entity.Property(s => s.IdServicio)
                      .ValueGeneratedOnAdd();

                entity.HasOne<Reservas>()
                      .WithMany()
                      .HasForeignKey(s => s.IdRecervaFK) 
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(s => s.Descripcion)
                      .HasMaxLength(500);
                entity.Property(s => s.Costo)
                      .IsRequired();
            });
        }

    }

    public class Clientes
    {
        public int? IdCliente { get; set; } 
        public required string nombre { get; set; }
        public string? telefono { get; set; }

        public string? direcccion { get; set; }

    }

    public class Habitaciones
    {
        public int? IdHabitacion { get; set; } 
        public required string numero { get; set; }

        public decimal? Precio { get; set; }
    }

    public class Reservas
    {
        public int? IdRecerva { get; set; }
        public required int IdHabitacionFK { get; set; } 
        public required int IdClienteFK { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFinal { get; set; }
    }

    public class ServiciosAdicionales
    {
        public int? IdServicio { get; set; }
        public required int IdRecervaFK { get; set; } 
        public string? Descripcion { get; set; }
        public decimal? Costo { get; set; }
    }
}

