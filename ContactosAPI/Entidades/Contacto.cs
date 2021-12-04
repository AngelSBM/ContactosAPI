using System.ComponentModel.DataAnnotations;

namespace ContactosAPI.Entidades
{
    public class Contacto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        [Required]
        public List<Correo> Correos { get; set; }
        [Required]
        public List<Telefono> Telefonos { get; set; }
        public bool Mostrar { get; set; } = true;
    }
}
