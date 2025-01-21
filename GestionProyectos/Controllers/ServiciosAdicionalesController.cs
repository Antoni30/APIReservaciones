using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GestionProyectos.Models;

namespace GestionProyectos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosAdicionalesController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;

        public ServiciosAdicionalesController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiciosAdicionales()
        {
            var serviciosAdicionales = await _appDbContext.ServiciosAdicionales.ToListAsync();
            return Ok(serviciosAdicionales);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServicioAdicional(ServiciosAdicionales servicioAdicional)
        {
            if (servicioAdicional.Costo < 0)
                return BadRequest("El costo del servicio adicional no puede ser negativo.");

            var reservaExistente = await _appDbContext.Reserva.FindAsync(servicioAdicional.IdRecervaFK);
            if (reservaExistente == null)
                return NotFound("La reserva especificada no existe.");

            _appDbContext.ServiciosAdicionales.Add(servicioAdicional);
            await _appDbContext.SaveChangesAsync();
            return Ok(servicioAdicional);
        }

        [HttpPut]
        public async Task<IActionResult> EditarServicioAdicional(ServiciosAdicionales servicioAdicional)
        {
            var servicioExistente = await _appDbContext.ServiciosAdicionales.FindAsync(servicioAdicional.IdServicio);

            if (servicioExistente == null)
                return NotFound("El servicio adicional no existe.");

            if (servicioAdicional.Costo < 0)
                return BadRequest("El costo del servicio adicional no puede ser negativo.");

            var reservaExistente = await _appDbContext.Reserva.FindAsync(servicioAdicional.IdRecervaFK);
            if (reservaExistente == null)
                return NotFound("La reserva especificada no existe.");

            servicioExistente.Descripcion = servicioAdicional.Descripcion;
            servicioExistente.Costo = servicioAdicional.Costo;
            servicioExistente.IdRecervaFK = servicioAdicional.IdRecervaFK;

            await _appDbContext.SaveChangesAsync();
            return Ok(servicioExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicioAdicional(int id)
        {
            var servicioExistente = await _appDbContext.ServiciosAdicionales.FindAsync(id);

            if (servicioExistente == null)
                return NotFound("El servicio adicional no existe.");

            _appDbContext.ServiciosAdicionales.Remove(servicioExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok($"Servicio adicional con ID {id} eliminado correctamente.");
        }
    }
}
