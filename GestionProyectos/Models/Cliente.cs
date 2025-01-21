namespace GestionProyectos.Models
{
    public class Cliente
    {
        public int? IdCliente { get; set; }
        public required string nombre { get; set; }
        public string? telefono { get; set; }

        public string? direcccion { get; set; }
    }
}
