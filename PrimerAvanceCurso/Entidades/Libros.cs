using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Entidades
{
    public class Libros
    {

        public int Id { get; set; } 
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        public string FechaPublicacion { get; set; }
        public string Portada { get; set; } 

    }
}
