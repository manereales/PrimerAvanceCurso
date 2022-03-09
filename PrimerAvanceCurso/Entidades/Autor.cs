using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Entidades
{
    public class Autor
    {
        public int Id { get; set; } 
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public string Foto { get; set; }  
    }
}
