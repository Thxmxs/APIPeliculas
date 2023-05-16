using APIpeliculas.Entities;
using APIpeliculas.Helpers;
using APIpeliculas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIpeliculas.Controllers
{
    [Route("api/actores")]
    [ApiController]
    public class ActoresController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorAzureStorage almacenadorAzureStorage;

        public ActoresController(ApplicationDbContext context, IMapper mapper,IAlmacenadorAzureStorage almacenadorAzureStorage)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorAzureStorage = almacenadorAzureStorage;
        }
        [HttpGet]
        public async Task<ActionResult<List<ActorViewModel>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var actores = await queryable.OrderBy(x => x.Nombre).Paginar(paginacion).ToListAsync();
            return mapper.Map<List<ActorViewModel>>(actores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorViewModel>> Get(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id); 

            if(actor is null)
            {
                return NotFound();
            }
            return mapper.Map<ActorViewModel>(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ActorCreacionModel actorCreacionModel)
        {
            var actor = mapper.Map<Actor>(actorCreacionModel);
            if (actorCreacionModel.Foto is not null)
            {
                actor.Foto = await almacenadorAzureStorage.GuardarArchivo("actores", actorCreacionModel.Foto);
            } 
            context.Add(actor);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionModel actorCreacionModel)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actor is null)
            {
                return NotFound();
            }

            actor = mapper.Map(actorCreacionModel, actor);
            
            if(actorCreacionModel.Foto is not null)
            {
                actor.Foto = await almacenadorAzureStorage.EditarArchivo("actores", actorCreacionModel.Foto, actor.Foto);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if(actor is null)
            {
                return NotFound();
            }

            context.Remove(actor);
            await context.SaveChangesAsync();
            await almacenadorAzureStorage.BorrarArchivo(actor.Foto, "actores");
            return NoContent();
        }
    }
}
