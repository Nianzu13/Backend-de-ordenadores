using Inventario.Model; // Quitamos la S para que coincida con tu carpeta
using Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // IMPORTANTE: Si en phpMyAdmin tu tabla se llama "InventarioOrdenadores", déjalo así.
        // Si tu tabla se llama "ordenadores", cambia el nombre de abajo.
        public DbSet<Ordenador> InventarioOrdenadores { get; set; }
    }
}