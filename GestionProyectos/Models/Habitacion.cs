namespace GestionProyectos.Models
{
    public class Habitacion
    {
        public int? IdHabitacion { get; set; }
        public required string numero { get; set; }

        public decimal? Precio { get; set; }
    }
}
