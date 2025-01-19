using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestionProyectos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;

        public HabitacionController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetHabitaciones()
        {
            var habitaciones = await _appDbContext.Habitaciones.ToListAsync();
            return Ok(habitaciones);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHabitacion(Habitaciones habitacion)
        {
            if (string.IsNullOrEmpty(habitacion.numero))
                return BadRequest("El número de la habitación es obligatorio.");

            _appDbContext.Habitaciones.Add(habitacion);
            await _appDbContext.SaveChangesAsync();
            return Ok(habitacion);
        }

        [HttpPut]
        public async Task<IActionResult> EditarHabitacion(Habitaciones habitacion)
        {
            var habitacionExistente = await _appDbContext.Habitaciones.FindAsync(habitacion.IdHabitacion);

            if (habitacionExistente == null)
                return NotFound("La habitación no existe.");

            habitacionExistente.numero = habitacion.numero;

            await _appDbContext.SaveChangesAsync();
            return Ok(habitacionExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarHabitacion(int id)
        {
            var habitacionExistente = await _appDbContext.Habitaciones.FindAsync(id);

            if (habitacionExistente == null)
                return NotFound("La habitación no existe.");

            _appDbContext.Habitaciones.Remove(habitacionExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok($"Habitación con ID {id} eliminada correctamente.");
        }
    }
}
