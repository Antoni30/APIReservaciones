using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GestionProyectos.Models;

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
            var reservas = await _appDbContext.Reserva.ToListAsync();
            return Ok(reservas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReserva(Reserva reserva)
        {
            if (reserva.FechaInicio >= reserva.FechaFinal)
                return BadRequest("La fecha de inicio debe ser anterior a la fecha final.");

            if (reserva.FechaFinal <= reserva.FechaInicio)
                return BadRequest("La fecha de fin no puede ser anterior a la fecha inicial.");

            var habitacionExistente = await _appDbContext.Habitacion.FindAsync(reserva.IdHabitacionFK);
            if (habitacionExistente == null)
                return NotFound("La habitación especificada no existe.");

            bool habitacionOcupada = await _appDbContext.Reserva.AnyAsync(r =>
                r.IdHabitacionFK == reserva.IdHabitacionFK &&
                ((reserva.FechaInicio >= r.FechaInicio && reserva.FechaInicio < r.FechaFinal) ||
                 (reserva.FechaFinal > r.FechaInicio && reserva.FechaFinal <= r.FechaFinal) ||
                 (reserva.FechaInicio <= r.FechaInicio && reserva.FechaFinal >= r.FechaFinal))
            );

            if (habitacionOcupada)
                return Conflict("La habitación ya está ocupada para las fechas especificadas.");

            _appDbContext.Reserva.Add(reserva);
            await _appDbContext.SaveChangesAsync();
            return Ok(reserva);
        }

        [HttpPut]
        public async Task<IActionResult> EditarReserva(Reserva reserva)
        {
            var reservaExistente = await _appDbContext.Reserva.FindAsync(reserva.IdRecerva);

            if (reservaExistente == null)
                return NotFound("La reserva no existe.");

            if (reserva.FechaInicio >= reserva.FechaFinal)
                return BadRequest("La fecha de inicio debe ser anterior a la fecha final.");

            if (reserva.FechaFinal <= reserva.FechaInicio)
                return BadRequest("La fecha de fin no puede ser anterior a la fecha inicial.");

            var habitacionExistente = await _appDbContext.Habitacion.FindAsync(reserva.IdHabitacionFK);
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
            var reservaExistente = await _appDbContext.Reserva.FindAsync(id);

            if (reservaExistente == null)
                return NotFound("La reserva no existe.");

            _appDbContext.Reserva.Remove(reservaExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok($"Reserva con ID {id} eliminada correctamente.");
        }
    }
}
