using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Models;
using Inventario.Model;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenadoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdenadoresController(AppDbContext context)
        {
            _context = context;
        }

        // 1. VER TODOS (GET)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ordenador>>> GetAll()
        {
            return await _context.InventarioOrdenadores.ToListAsync();
        }

        // 2. BUSCADOR GENERAL (GET)
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Ordenador>>> Search([FromQuery] string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.InventarioOrdenadores.ToListAsync();
            }

            var t = termino.ToLower();

            return await _context.InventarioOrdenadores
                .Where(o => (o.Usuario != null && o.Usuario.ToLower().Contains(t)) ||
                            (o.Procesador != null && o.Procesador.ToLower().Contains(t)) ||
                            (o.etiqueta_inventario != null && o.etiqueta_inventario.ToLower().Contains(t)) ||
                            (o.Sistema_Operativo != null && o.Sistema_Operativo.ToLower().Contains(t)))
                .ToListAsync();
        }

        // 3. FILTRADO POR CAMPOS ESPECÍFICOS (GET)
        [HttpGet("filtrados")]
        public async Task<ActionResult<IEnumerable<Ordenador>>> GetFiltered(
            [FromQuery] string? usuario,
            [FromQuery] string? procesador,
            [FromQuery] string? sistema,
            [FromQuery] string? etiqueta)
        {
            var query = _context.InventarioOrdenadores.AsQueryable();

            if (!string.IsNullOrWhiteSpace(usuario))
                query = query.Where(o => o.Usuario != null && o.Usuario.ToLower().Contains(usuario.ToLower()));

            if (!string.IsNullOrWhiteSpace(procesador))
                query = query.Where(o => o.Procesador != null && o.Procesador.ToLower().Contains(procesador.ToLower()));

            if (!string.IsNullOrWhiteSpace(sistema))
                query = query.Where(o => o.Sistema_Operativo != null && o.Sistema_Operativo.ToLower().Contains(sistema.ToLower()));

            if (!string.IsNullOrWhiteSpace(etiqueta))
                query = query.Where(o => o.etiqueta_inventario != null && o.etiqueta_inventario.ToLower().Contains(etiqueta.ToLower()));

            return await query.ToListAsync();
        }

        // 4. AGREGAR (POST)
        [HttpPost]
        public async Task<ActionResult<Ordenador>> Create(Ordenador nuevo)
        {
            nuevo.Hora_de_agregacion = DateTime.Now;
            _context.InventarioOrdenadores.Add(nuevo);
            await _context.SaveChangesAsync();
            return Ok(nuevo);
        }

        // 5. ELIMINAR (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipo = await _context.InventarioOrdenadores.FindAsync(id);
            if (equipo == null) return NotFound();

            _context.InventarioOrdenadores.Remove(equipo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. EDITAR (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ordenador actualizado)
        {
            var equipo = await _context.InventarioOrdenadores.FindAsync(id);
            if (equipo == null) return NotFound();

            equipo.Usuario = actualizado.Usuario;
            equipo.Procesador = actualizado.Procesador;
            equipo.Sistema_Operativo = actualizado.Sistema_Operativo;
            equipo.Ram = actualizado.Ram;
            equipo.Capacidad_Disco = actualizado.Capacidad_Disco;
            equipo.Tipo_Disco = actualizado.Tipo_Disco;
            equipo.Precio = actualizado.Precio;
            equipo.url_imagen = actualizado.url_imagen;
            equipo.Estado = actualizado.Estado;
            equipo.mac_address = actualizado.mac_address;
            equipo.etiqueta_inventario = actualizado.etiqueta_inventario;
            equipo.Admin = actualizado.Admin;

            await _context.SaveChangesAsync();
            return Ok(equipo);
        }

        // 7. LOGIN
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest datos)
        {
            if (datos.Usuario == "Javier" && datos.Password == "1234")
            {
                return Ok(new { mensaje = "Bienvenido", token = "ABC-123", admin = true });
            }
            return Unauthorized();
        }
    }  // ← cierra OrdenadoresController

    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}  // ← cierra namespace-----------------------------