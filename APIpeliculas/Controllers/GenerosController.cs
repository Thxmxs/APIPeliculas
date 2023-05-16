using APIpeliculas.Entities;
using APIpeliculas.Helpers;
using APIpeliculas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIpeliculas.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GeneroViewModel>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Generos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);

            var generosList = await queryable.OrderBy(x => x.Nombre).Paginar(paginacion).ToListAsync();

            var generosListMapeados = mapper.Map<List<GeneroViewModel>>(generosList);
            return Ok(generosListMapeados);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Genero>> Get(int Id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == Id);

            if(genero is null)
            {
                return NotFound();
            }

            return mapper.Map<Genero>(genero);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromBody] GeneroCreacionViewModel generoCreacionViewModel)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if(genero is null)
            {
                return NotFound();
            }
            genero = mapper.Map(generoCreacionViewModel, genero);

            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (genero is null)
            {
                return NotFound();
            }
            context.Generos.Remove(genero);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Genero genero)
        {
            context.Add(genero);
            await context.SaveChangesAsync();

            return NoContent();
        }


    }
}
