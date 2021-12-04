using AutoMapper;
using ContactosAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactosAPI.Controllers
{
    [ApiController]
    [Route("api/telefonos")]
    public class TelefonoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TelefonoController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<TelefonoDTO>>> Get()
        {
            var telefonos= await context.Telefonos.ToListAsync();
            return mapper.Map<List<TelefonoDTO>>(telefonos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<TelefonoDTO>>> Get(int id)
        {
            var telefonos = await context.Telefonos
                .Where(x => x.ContactoId == id)
                .ToListAsync();
            return mapper.Map<List<TelefonoDTO>>(telefonos);
        }


    }
}
