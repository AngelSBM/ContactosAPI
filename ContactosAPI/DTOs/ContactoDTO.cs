using ContactosAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ContactosAPI.DTOs
{
    public class ContactoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }

        public List<CorreoDTO> Correos { get; set; }

        public List<TelefonoDTO> Telefonos { get; set; }
        public bool Mostrar { get; set; } = true;
    }
}
