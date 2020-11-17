using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceMyBet2.Models
{
    public class ApuestaExamen
    {
        public string Equipo { get;}
        public double cantidadTotal { get; }

        public ApuestaExamen(string equipo, double cantidadTotal)
        {
            Equipo = equipo;
            this.cantidadTotal = cantidadTotal;
        }

        public ApuestaExamen()
        {
        }
    }
}