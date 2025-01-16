using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;

        public CategoriaController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Obtener todas las categorías
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _appDbContext.Categorias.ToListAsync();  // Ya no necesitamos Include para productos
            return Ok(categorias);
        }

        // Obtener una categoría por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriaById(int id)
        {
            var categoria = await _appDbContext.Categorias
                                               .FirstOrDefaultAsync(c => c.Id == id);
            if (categoria == null)
                return NotFound("Categoría no encontrada");

            return Ok(categoria);
        }

        // Crear una nueva categoría
        [HttpPost]
        public async Task<IActionResult> CreateCategoria(Categoria categoria)
        {
            if (categoria == null)
                return BadRequest("Los datos de la categoría no son válidos");

            _appDbContext.Categorias.Add(categoria);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoriaById), new { id = categoria.Id }, categoria);
        }

        // Actualizar una categoría existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest("El ID de la categoría no coincide");

            var categoriaExistente = await _appDbContext.Categorias.FindAsync(id);
            if (categoriaExistente == null)
                return NotFound("Categoría no encontrada");

            categoriaExistente.Nombre = categoria.Nombre;

            await _appDbContext.SaveChangesAsync();

            return NoContent(); // Devuelve 204 (sin contenido) para indicar éxito
        }

        // Eliminar una categoría por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _appDbContext.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound("Categoría no encontrada");

            _appDbContext.Categorias.Remove(categoria);
            await _appDbContext.SaveChangesAsync();

            return Ok($"Categoría con ID {id} eliminada correctamente");
        }
    }
}
