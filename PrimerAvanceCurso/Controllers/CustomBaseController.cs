using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerAvanceCurso.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class
        {
            var entidades = await context.Set<TEntidad>().AsNoTracking().ToListAsync();

            return mapper.Map<List<TDTO>>(entidades);
        }

        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId
        {

            var entidades = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entidades ==  null)
            {
                return NotFound();
            }

            return mapper.Map<TDTO>(entidades);
        }

        protected async Task<ActionResult> Post<TCreacion, TEntidad, TDTO>(TCreacion creacion, string nombreRuta) where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(creacion);

            context.Add(entidad);

            await context.SaveChangesAsync();

            var dtos = mapper.Map<TDTO>(entidad);

            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, dtos);

        }

        protected async Task<ActionResult> Put<TCreacion, TEntidad>(TCreacion creacion, int id) where TEntidad : class, IId
        {
            var existe = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existe == null)
            {
                return NotFound();
            }

            mapper.Map(creacion, existe);

            await context.SaveChangesAsync();

            return NoContent();
        }

        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new()
        {
            var existe = await context.Set<TEntidad>().AsNoTracking().AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TEntidad() { Id = id });

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
