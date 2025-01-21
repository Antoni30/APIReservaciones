using GestionProyectos.Models;
using Microsoft.EntityFrameworkCore;


namespace GestionProyectos
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Habitacion> Habitacion { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<ServiciosAdicionales> ServiciosAdicionales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.IdCliente);
                entity.Property(c => c.IdCliente)
                      .ValueGeneratedOnAdd();
                entity.Property(c => c.nombre)
                      .IsRequired();
            });

            modelBuilder.Entity<Habitacion>(entity =>
            {
                entity.HasKey(h => h.IdHabitacion);
                entity.Property(h => h.IdHabitacion)
                      .ValueGeneratedOnAdd();
                entity.Property(h => h.numero)
                      .IsRequired();
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(r => r.IdRecerva);
                entity.Property(r => r.IdRecerva)
                      .ValueGeneratedOnAdd(); 

                entity.HasOne<Habitacion>()
                      .WithMany()
                      .HasForeignKey(r => r.IdHabitacionFK) 
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Cliente>()
                      .WithMany()
                      .HasForeignKey(r => r.IdClienteFK)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ServiciosAdicionales>(entity =>
            {
                entity.HasKey(s => s.IdServicio);
                entity.Property(s => s.IdServicio)
                      .ValueGeneratedOnAdd();

                entity.HasOne<Reserva>()
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
}

