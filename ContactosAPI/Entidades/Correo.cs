using System.ComponentModel.DataAnnotations;

namespace ContactosAPI.Entidades
{
    public class Correo
    {
        public int Id { get; set; }
        [Required]
        public string direccionCorreo { get; set; }
        public int ContactoId { get; set; }
        public Contacto Contacto { get; set; }
    }
}
