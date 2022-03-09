using Microsoft.AspNetCore.Http;
using PrimerAvanceCurso.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.DTO_s
{
    public class AutoresCreacionDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }  
    }
}
