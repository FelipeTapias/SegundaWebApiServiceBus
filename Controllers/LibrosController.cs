using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SegundaWebAPI.Entities;

namespace SegundaWebAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await _context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
            if(libro == null)
                return NotFound();

            return libro;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await _context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor)
                return BadRequest($"No exite el autor con el id: {libro.AutorId}");

            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
