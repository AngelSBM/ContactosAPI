using AutoMapper;
using ContactosAPI.DTOs;
using ContactosAPI.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactosAPI.Controllers
{
    [ApiController]
    [Route("api/contactos")]
    public class ContactosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ContactosController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactoDTO>>> Get()
        {
            var contactos = await context.Contactos
                .Where(x => x.Mostrar)
                .Include(x => x.Correos)
                .Include(x => x.Telefonos)
                .ToListAsync();
            return mapper.Map<List<ContactoDTO>>(contactos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Contacto>> Get(int id)
        {
            var contacto = await context.Contactos
                .Where(x => x.Mostrar)
                .Include(x => x.Correos)
                .Include(x => x.Telefonos)
                .FirstOrDefaultAsync(x => x.Id == id);         
            return contacto;
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> Filtrar([FromQuery] string nombre)
        {
            var contactosQueryable = context.Contactos.AsQueryable();
            if (!string.IsNullOrEmpty(nombre))
            {
                contactosQueryable = contactosQueryable
                    .Where(x => x.Mostrar)
                    .Where(x => x.Nombre.Contains(nombre));
            }
            
            return Ok(contactosQueryable);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContactoCreacionDTO contactoCreacionDTO)
        {

            if(contactoCreacionDTO.Correos.Count() == 0 && contactoCreacionDTO.Correos.Count() == 0)
            {
                return BadRequest("Debe registrarse con por lo menos un correo o un telefono");
            }

            var contacto = mapper.Map<Contacto>(contactoCreacionDTO);

            context.Contactos.Add(contacto);
            await context.SaveChangesAsync();

            return Ok(contacto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] ContactoActualizacion contactoActualizacion, int id)
        {

            var existe = await context.Contactos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }
            var contacto = mapper.Map<Contacto>(contactoActualizacion);
            contacto.Id = id;

            context.Update(contacto);

            await context.SaveChangesAsync();
            return Ok("Contacto actualizado");
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ContactoPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var contacto = await context.Contactos.FirstOrDefaultAsync(x => x.Id == id);

            if (contacto == null) { return NotFound(); }

            var contactoDTO = mapper.Map<ContactoPatchDTO>(contacto);

            patchDocument.ApplyTo(contactoDTO, ModelState);

            var esValido = TryValidateModel(contactoDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(contactoDTO, contacto);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Contactos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            if(context.Contactos.Where(x => x.Mostrar).Count() == 1) { return BadRequest("Oops! se debe quedar con por lo menos un contacto registrado."); }
            var contacto = await context.Contactos.FirstOrDefaultAsync(x => x.Id == id);
            contacto.Mostrar = false;
            await context.SaveChangesAsync();
            return Ok("Contacto Eliminado");
        }

    }
}
