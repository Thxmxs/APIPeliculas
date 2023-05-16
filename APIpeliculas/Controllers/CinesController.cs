using APIpeliculas.Entities;
using APIpeliculas.Helpers;
using APIpeliculas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIpeliculas.Controllers
{
    [Route("api/cines")]
    [ApiController]
    public class CinesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinesCreacionModel cinesCreacionModel)
        {
            var cine = mapper.Map<Cine>(cinesCreacionModel);
            context.Add(cine);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult<List<CineViewModel>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Cines.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var cines = await queryable.OrderBy(x => x.Nombre).Paginar(paginacion).ToListAsync();
            return mapper.Map<List<CineViewModel>>(cines);
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CineViewModel>> Get(int Id)
        {
            var cine = await context.Cines.FirstOrDefaultAsync(x => x.Id == Id);

            if (cine is null)
            {
                return NotFound();
            }

            return mapper.Map<CineViewModel>(cine);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CinesCreacionModel cinesCreacionModel)
        {
            var cine = await context.Cines.FirstOrDefaultAsync(x => x.Id == id);

            if (cine is null)
            {
                return NotFound();
            }
            cine = mapper.Map(cinesCreacionModel, cine);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Cines.AnyAsync(x => x.Id == id);

            if (!existe){
                return NotFound();
            }

            context.Remove(new Cine() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
