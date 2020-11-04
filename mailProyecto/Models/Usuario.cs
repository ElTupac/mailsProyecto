using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mailProyecto.Models
{
    public class Usuario
    {
        [Key]
        public string Mail {get; set;}
        [Required]
        public string Nombre {get; set;}
        public List<Nota> Notas{get; set;}
    }
}