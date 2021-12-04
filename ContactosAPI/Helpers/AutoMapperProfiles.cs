using AutoMapper;
using ContactosAPI.DTOs;
using ContactosAPI.Entidades;

namespace ContactosAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Contacto, ContactoDTO>()
                .ForMember(x => x.Correos, options => options.MapFrom(MapearCorreos))
                .ForMember(x => x.Telefonos, options => options.MapFrom(MapearTelefonos));

            CreateMap<ContactoCreacionDTO, Contacto>()
                .ForMember(x => x.Correos, options => options.MapFrom(MapearCreacionCorreos))
                .ForMember(x => x.Telefonos, options => options.MapFrom(MapearCreacionTelefonos));

            CreateMap<ContactoPatchDTO, Contacto>().ReverseMap();
            CreateMap<ContactoActualizacion, Contacto>().ReverseMap();


            CreateMap<Correo, CorreoDTO>().ReverseMap();
            CreateMap<Correo, CorreoCreacionDTO>().ReverseMap();


            CreateMap<Telefono, TelefonoCreacionDTO>().ReverseMap();
            CreateMap<Telefono, TelefonoDTO>().ReverseMap();
        }


        private List<CorreoDTO> MapearCorreos(Contacto contacto, ContactoDTO contactoDTO)
        {
            var resultado = new List<CorreoDTO>();

            foreach (var cr in contacto.Correos)
            {
                resultado.Add(new CorreoDTO()
                {
                    Id = cr.Id,
                    direccionCorreo = cr.direccionCorreo,
                    ContactoId = cr.ContactoId
                });
            }

            return resultado;
        }

        private List<TelefonoDTO> MapearTelefonos(Contacto contacto, ContactoDTO contactoDTO)
        {
            var resultado = new List<TelefonoDTO>();

            foreach (var tl in contacto.Telefonos)
            {
                resultado.Add(new TelefonoDTO()
                {
                    Id = tl.Id,
                    numeroTelefono = tl.numeroTelefono,
                    ContactoId = tl.ContactoId
                });
            }

            return resultado;
        }

        private List<Correo> MapearCreacionCorreos(ContactoCreacionDTO contactoCreacionDTO, Contacto contacto)
        {
            var resultado = new List<Correo>();

            foreach (var cr in contactoCreacionDTO.Correos)
            {
                resultado.Add(new Correo()
                {
                    direccionCorreo = cr.direccionCorreo
                });
            }

            return resultado;
        }

        private List<Telefono> MapearCreacionTelefonos(ContactoCreacionDTO contactoCreacionDTO, Contacto contacto)
        {
            var resultado = new List<Telefono>();

            foreach (var tl in contactoCreacionDTO.Telefonos)
            {
                resultado.Add(new Telefono()
                {
                    numeroTelefono = tl.numeroTelefono
                });
            }

            return resultado;
        }
    }
}
