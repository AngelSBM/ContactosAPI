using ContactosAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ContactosAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Contacto> Contactos {  get; set; }
        public DbSet<Correo> Correos { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
    }
}
