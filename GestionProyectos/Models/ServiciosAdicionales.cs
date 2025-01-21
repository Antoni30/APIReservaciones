namespace GestionProyectos.Models
{
    public class ServiciosAdicionales
    {
        public int? IdServicio { get; set; }
        public required int IdRecervaFK { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Costo { get; set; }
    }
}
