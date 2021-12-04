using System.ComponentModel.DataAnnotations;

namespace ContactosAPI.DTOs
{
    public class ContactoPatchDTO
    {
        [Required]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        [Required]
        public List<CorreoCreacionDTO> Correos { get; set; }
        [Required]
        public List<TelefonoCreacionDTO> Telefonos { get; set; }
    }
}
