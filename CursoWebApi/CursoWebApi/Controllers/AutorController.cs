using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoWebApi.Contexts;
using CursoWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AutorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return _context.Autores.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public ActionResult<Autor> Get(int id)
        {
            var autor = _context.Autores.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            _context.Autores.Add(autor);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id}, autor);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            _context.Entry(autor).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = _context.Autores.FirstOrDefault(x => x.Id == id);
            _context.Autores.Remove(autor!);
            _context.SaveChanges();
            return autor;

        }
    }
}
