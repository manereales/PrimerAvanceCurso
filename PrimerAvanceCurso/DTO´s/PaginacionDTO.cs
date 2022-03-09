using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerAvanceCurso.DTO_s
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;

        private int numeroRegistroPagina = 10;
        private readonly int numeroMaximoRegistroPagina = 20;

        public int NumeroResgistroPagina
        {

            get => numeroRegistroPagina;


            //set => this.NumeroMaximoRegistrosPagina = (value > numeroRegistroPagina) ? numeroRegistroPagina : value;

            set {

                if (value >= NumeroResgistroPagina)
                {
                    numeroRegistroPagina = numeroMaximoRegistroPagina;
                }

                numeroRegistroPagina = value;
            }
               

                
        }


    }
}
