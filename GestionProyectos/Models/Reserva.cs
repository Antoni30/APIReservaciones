namespace GestionProyectos.Models
{
    public class Reserva
    {
        public int? IdRecerva { get; set; }
        public required int IdHabitacionFK { get; set; }
        public required int IdClienteFK { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFinal { get; set; }
    }
}
