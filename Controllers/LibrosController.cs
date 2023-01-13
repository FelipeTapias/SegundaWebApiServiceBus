﻿using Adapters;
using Adapters.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SegundaWebAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SegundaWebAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ISenderMessage senderMessage;

        public LibrosController(ApplicationDbContext context,
                                ISenderMessage senderMessage)
        {
            this.context = context;
            this.senderMessage = senderMessage;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
            if(libro == null)
                return NotFound();

            return libro;
        }
        //[HttpPost]        
        //public async Task<ActionResult> Post(Libro libro)
        //{
        //    var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
        //    if(!existeAutor)
        //        return BadRequest($"No exite el autor con el id: { libro.AutorId }");

        //    context.Add(libro);
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string message)
        {
            await senderMessage.CreateMessage(message);
            return Ok();
        }
    }
}