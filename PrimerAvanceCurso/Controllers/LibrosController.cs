using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerAvanceCurso.DTO_s;
using PrimerAvanceCurso.Entidades;
using PrimerAvanceCurso.Helpers;
using PrimerAvanceCurso.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacendaroArchivos almacenadorArchivos;
        private readonly string contenedor = "libros";

        public LibrosController(ApplicationDbContext context, IMapper mapper, IAlmacendaroArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }


        [HttpGet]
        public async Task<List<LibrosDTO>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable =  context.Libros.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.NumeroResgistroPagina);

            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();

            return mapper.Map<List<LibrosDTO>>(entidades);
        }


        [HttpGet("{id}", Name ="obtenerLibro")]
        public async Task<ActionResult<LibrosDTO>> Get(int id)
        {
            var libros = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (libros == null )
            {
                return NotFound();
            }

            return mapper.Map<LibrosDTO>(libros);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] LibrosCreacionDTO librosCreacionDTO)
        {
            var libros = mapper.Map<Libros>(librosCreacionDTO);

            if (librosCreacionDTO.Portada != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await librosCreacionDTO.Portada.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(librosCreacionDTO.Portada.FileName);
                    libros.Portada = await almacenadorArchivos.GuardarArchivo(contenido, contenedor,extension, librosCreacionDTO.Portada.ContentType);
                }
            }

            context.Add(libros);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("obtenerLibro", new Libros() { Id = libros.Id });

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] LibrosCreacionDTO librosCreacionDTO)
        {
            var librosDB = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (librosDB == null)
            {
                return NotFound();
            }

            librosDB = mapper.Map(librosCreacionDTO, librosDB);

            if (librosCreacionDTO.Portada != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await librosCreacionDTO.Portada.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(librosCreacionDTO.Portada.FileName);

                    librosDB.Portada = await almacenadorArchivos.ModificarArchivo(contenido, contenedor, extension, librosCreacionDTO.Portada.ContentType, librosDB.Portada);
                }
            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<LibrosPatchDTO> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }

            var librodb = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (librodb == null)
            {
                return NotFound();
            }

            var entidad = mapper.Map<LibrosPatchDTO>(librodb);

            jsonPatchDocument.ApplyTo(entidad, ModelState);

            var esValido = TryValidateModel(entidad);

            if (!esValido)
            {
                return BadRequest();
            }

            mapper.Map(entidad, librodb);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Libros() { Id = id});
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
