using Microsoft.AspNetCore.Http;
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
    public class LibroController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LibroController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Index()
        {
            return _context.Libros.Include(x => x.Autor).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = _context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new {id = libro.Id}, libro);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = _context.Libros.FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            _context.Libros.Remove(libro);
            _context.SaveChanges();
            return libro;
        }

    }
}
