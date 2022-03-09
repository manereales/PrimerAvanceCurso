using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Servicios
{
    public interface IAlmacendaroArchivos
    {
        Task BorrarArchivo(string ruta, string contenedor);
        Task<string> ModificarArchivo(byte[] contenido, string contenedor, string extension, string contentType, string ruta);
        Task<string> GuardarArchivo(byte[] contenido, string contenedor, string extension, string contentType);
    }
}
