using System.ComponentModel.DataAnnotations;

namespace ContactosAPI.Entidades
{
    public class Telefono
    {
        public int Id { get; set; }
        [Required]
        public string numeroTelefono { get; set; }
        public int ContactoId { get; set; }
        public Contacto Contacto {  get; set; }
    }
}
