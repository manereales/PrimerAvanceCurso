using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Validaciones
{
    public class PesoArchivoValidacion: ValidationAttribute
    {
        private readonly int pesoMaximoEnBytes;

        public PesoArchivoValidacion(int pesoMaximoEnBytes )
        {
            this.pesoMaximoEnBytes = pesoMaximoEnBytes;
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

            if (formFile.Length > pesoMaximoEnBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso maximo en bytes es: {pesoMaximoEnBytes} mb" );
            }

            return ValidationResult.Success;
        }
    }
}
