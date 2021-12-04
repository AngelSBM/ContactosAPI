using AutoMapper;
using ContactosAPI.DTOs;
using ContactosAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactosAPI.Controllers
{
    [ApiController]
    [Route("api/correos")]
    public class CorreoController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CorreoController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<CorreoDTO>>> Get()
        {
            var correos = await context.Correos.ToListAsync();
            return mapper.Map<List<CorreoDTO>>(correos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<CorreoDTO>>> Get(int id)
        {
            var correos = await context.Correos
                .Where(x => x.ContactoId == id)
                .ToListAsync();
            return mapper.Map<List<CorreoDTO>>(correos);
        }

        [HttpPut("{contactoid:int}/{id:int}")]
        public async Task<ActionResult> Post(CorreoCreacionDTO correoActualizado, int contactoid, int id)
        {
            var existe = await context.Correos.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }

            var correo = mapper.Map<Correo>(correoActualizado);
            correo.Id = id;
            correo.ContactoId = contactoid;

            context.Update(correo);

            await context.SaveChangesAsync();
            return Ok();


        }
    }
}
