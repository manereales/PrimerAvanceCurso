using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.DTO_s
{
    public class LibrosCreacionDTO
    { 
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        public string FechaPublicacion { get; set;}
        public IFormFile Portada { get; set; }
    }
}
