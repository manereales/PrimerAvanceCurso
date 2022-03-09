using Microsoft.EntityFrameworkCore;
using PrimerAvanceCurso.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Libros> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }

    }
}
