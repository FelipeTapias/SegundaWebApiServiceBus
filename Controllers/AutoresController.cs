using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SegundaWebAPI.entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SegundaWebAPI.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")]
        public async  Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> PrimerAutor()
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }else
            {
                return autor;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            var autorE = await context.Autores.AnyAsync(x => x.Id == id);

            if (autor.Id != id)
                return BadRequest("El id del autor no coincide con el id de la URL");

            if (!autorE)
                return NotFound();

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var autor = await context.Autores.AnyAsync(x => x.Id == id);
            if (!autor)
                return NotFound();

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
