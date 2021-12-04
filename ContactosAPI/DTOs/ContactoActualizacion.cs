using System.ComponentModel.DataAnnotations;

namespace ContactosAPI.DTOs
{
    public class ContactoActualizacion
    {
        [Required]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }

    }
}
