using AutoMapper;
using PrimerAvanceCurso.DTO_s;
using PrimerAvanceCurso.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.Helpers
{
    public class Automapper: Profile
    {
        public Automapper()
        {
            CreateMap<Libros, LibrosDTO>().ReverseMap();
            CreateMap<LibrosCreacionDTO, Libros>();
        }
    }
}
