using Microsoft.EntityFrameworkCore;
using SegundaWebAPI.entities;
using SegundaWebAPI.Entities;

namespace SegundaWebAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
