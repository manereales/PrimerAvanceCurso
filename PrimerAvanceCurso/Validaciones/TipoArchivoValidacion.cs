using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Validaciones
{
    public class TipoArchivoValidacion: ValidationAttribute
    {
        private readonly string[] tipoArchivo;

        public TipoArchivoValidacion(string[] tipoArchivo)
        {
            this.tipoArchivo = tipoArchivo;
        }

        public TipoArchivoValidacion(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                tipoArchivo = new string[] {"image/jpeg", "image/png", "image/gif"  };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (!tipoArchivo.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo de archivo no es valido{string.Join("", tipoArchivo)}");
            }

            return ValidationResult.Success;
        }
    }
}
