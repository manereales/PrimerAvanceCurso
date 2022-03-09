using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacendaroArchivos
    {
        private readonly IHttpContextAccessor httpContextAccesor;
        private readonly IWebHostEnvironment env;

        public AlmacenadorArchivosLocal(IHttpContextAccessor httpContextAccesor, IWebHostEnvironment env)
        {
            this.httpContextAccesor = httpContextAccesor;
            this.env = env;
        }


        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                string nombreArchivo = Path.GetFileName(ruta);
                string folder = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);

                if (File.Exists(folder))
                {
                    File.Delete(folder);
                }

            }

            return Task.FromResult(0);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string contenedor, string extension, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}, {extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);

            await File.WriteAllBytesAsync(ruta, contenido);

            var urlActual = $"{httpContextAccesor.HttpContext.Request.Scheme}:// {httpContextAccesor.HttpContext.Request.Host}";
            var urlDb = Path.Combine(urlActual, nombreArchivo, contenedor).Replace("\\", "/");

            return urlDb;
        }

        public async Task<string> ModificarArchivo(byte[] contenido, string contenedor, string extension, string contentType, string ruta)
        {
             await BorrarArchivo(ruta, contenedor);
            return await  GuardarArchivo(contenido, contenedor, extension, contentType);
        }
    }
}
