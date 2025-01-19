using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestionProyectos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;

        public ReservaController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }
 
        [HttpGet]
        public async Task<IActionResult> GetReservas()
        {
            var reservas = await _appDbContext.Reservas.ToListAsync();
            return Ok(reservas);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReserva(Reservas reserva)
        {
            if (reserva.FechaInicio >= reserva.FechaFinal)
                return BadRequest("La fecha de inicio debe ser anterior a la fecha final.");

            var habitacionExistente = await _appDbContext.Habitaciones.FindAsync(reserva.IdHabitacionFK);
            if (habitacionExistente == null)
                return NotFound("La habitación especificada no existe.");

            _appDbContext.Reservas.Add(reserva);
            await _appDbContext.SaveChangesAsync();
            return Ok(reserva);
        }
        [HttpPut]
        public async Task<IActionResult> EditarReserva(Reservas reserva)
        {
            var reservaExistente = await _appDbContext.Reservas.FindAsync(reserva.IdRecerva);

            if (reservaExistente == null)
                return NotFound("La reserva no existe.");

            if (reserva.FechaInicio >= reserva.FechaFinal)
                return BadRequest("La fecha de inicio debe ser anterior a la fecha final.");

            var habitacionExistente = await _appDbContext.Habitaciones.FindAsync(reserva.IdHabitacionFK);
            if (habitacionExistente == null)
                return NotFound("La habitación especificada no existe.");

            reservaExistente.IdHabitacionFK = reserva.IdHabitacionFK;
            reservaExistente.FechaInicio = reserva.FechaInicio;
            reservaExistente.FechaFinal = reserva.FechaFinal;

            await _appDbContext.SaveChangesAsync();
            return Ok(reservaExistente);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarReserva(int id)
        {
            var reservaExistente = await _appDbContext.Reservas.FindAsync(id);

            if (reservaExistente == null)
                return NotFound("La reserva no existe.");

            _appDbContext.Reservas.Remove(reservaExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok($"Reserva con ID {id} eliminada correctamente.");
        }
    }
}
