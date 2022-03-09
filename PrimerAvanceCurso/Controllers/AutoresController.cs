using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerAvanceCurso.DTO_s;
using PrimerAvanceCurso.Entidades;
using PrimerAvanceCurso.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacendaroArchivos almacenadorArchivos;
        private readonly string contenedor = "autor";

        public AutoresController(ApplicationDbContext context, IMapper mapper, IAlmacendaroArchivos almacenadorArchivos  )
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<List<AutoresDTO>> Get()
        {
            var autores = await context.Autores.ToListAsync();

            var dtos = mapper.Map<List<AutoresDTO>>(autores);

            return dtos;
        }

        [HttpGet("{id}", Name = "obtenerAutor")]
        public async Task<ActionResult<AutoresDTO>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            var autordto = mapper.Map<AutoresDTO>(autor);

            return autordto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] AutoresCreacionDTO autoresCreacionDTO)
        {
            var autor = mapper.Map<Autor>(autoresCreacionDTO);

            if (autoresCreacionDTO.Foto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    await autoresCreacionDTO.Foto.CopyToAsync(memory);
                    var contenido = memory.ToArray();
                    var extension = Path.GetExtension(autoresCreacionDTO.Foto.FileName);
                    autor.Foto = await almacenadorArchivos.GuardarArchivo(contenido, contenedor, extension, autoresCreacionDTO.Foto.ContentType);
                }
            }

            context.Add(new Autor());
            await context.SaveChangesAsync();

            var autordto = mapper.Map<AutoresDTO>(autor);
            return new CreatedAtRouteResult("obtenerAutor", new Autor() { Id = autordto.Id }, autordto);

        }


        //[HttpPatch("{id}")]
        //public async Task<ActionResult> Patch(int id, JsonPatchDocument<AutoresPatchDTO> jsonPatchDocument)
        //{
        //    if (jsonPatchDocument == null)
        //    {
        //        return BadRequest();
        //    }

        //    var autordb = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

        //    if (autordb == null)
        //    {
        //        return NotFound();
        //    }

        //    var entidadDB = mapper.Map<AutoresPatchDTO>(autordb);

        //    PatchDocument.ApplyTo(entidadDB, ModelState);

        //    var esValido = TryValidateModel(entidadDB);

        //    if (!esValido)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    mapper.Map(entidadDB, autordb);

        //    await context.SaveChangesAsync();

        //    return NoContent();
            
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] AutoresCreacionDTO autoresCreacionDTO)
        {


            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            autor = mapper.Map(autoresCreacionDTO, autor);

            if (autoresCreacionDTO.Foto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    await autoresCreacionDTO.Foto.CopyToAsync(memory);
                    var contenido = memory.ToArray();
                    var extension = Path.GetExtension(autoresCreacionDTO.Foto.FileName);
                    autor.Foto = await almacenadorArchivos.ModificarArchivo(contenido, contenedor, extension, autoresCreacionDTO.Foto.ContentType, autor.Foto);

                }

            }

            await context.SaveChangesAsync();

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();

        }
    }
}
