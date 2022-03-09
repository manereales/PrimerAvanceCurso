using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.DTO_s
{
    public class AutoresPatchDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public string Foto { get; set; }
    }
}
