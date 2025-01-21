using Microsoft.AspNetCore.Mvc;
using GestionProyectos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public ClienteController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _appDbContext.Cliente.ToListAsync();
            return Ok(clientes);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCliente(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.nombre))
                return BadRequest("El nombre del cliente es obligatorio.");

            _appDbContext.Cliente.Add(cliente);
            await _appDbContext.SaveChangesAsync();
            return Ok(cliente);
        }
        [HttpPut]
        public async Task<IActionResult> EditarCliente(Cliente cliente)
        {
            var clienteExistente = await _appDbContext.Cliente.FindAsync(cliente.IdCliente);

            if (clienteExistente == null)
                return NotFound("El cliente no existe.");

            clienteExistente.nombre = cliente.nombre;
            clienteExistente.telefono = cliente.telefono;

            await _appDbContext.SaveChangesAsync();
            return Ok(clienteExistente);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var clienteExistente = await _appDbContext.Cliente.FindAsync(id);

            if (clienteExistente == null)
                return NotFound("El cliente no existe.");

            var reservaExistente = _appDbContext.Reserva.FirstOrDefault(reserva => reserva.IdClienteFK == id);

            if (reservaExistente != null)
            {
                return NotFound("El cliente tiene reservas pendientes.");
            }

            _appDbContext.Cliente.Remove(clienteExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok($"Cliente con ID {id} eliminado correctamente.");
        }
    }
}
