using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_PlaceMyBet.Models
{
    public class Mercado
    {
        #region Atributos

        public int idMercado;
        public double overUnder;
        public double cuotaOver;
        public double cuotaUnder;
        public double dineroOver;
        public double dineroUnder;
        public int idEvento;

        #endregion

        #region Constructores

        public Mercado(int idMercado, double overUnder, double cuotaOver, double cuotaUnder, double dineroOver, double dineroUnder, int idEvento)
        {
            this.idMercado = idMercado;
            this.overUnder = overUnder;
            this.cuotaOver = cuotaOver;
            this.cuotaUnder = cuotaUnder;
            this.dineroOver = dineroOver;
            this.dineroUnder = dineroUnder;
            this.idEvento = idEvento;
        }

        public double CuotaOver { get => cuotaOver; }
        public double CuotaUnder { get => cuotaUnder; }

        public Mercado()
        {
        }
        #endregion

        #region Clase MercadoDTO
    }
    public class MercadoDTO
    {
        public double overUnder;
        public double cuotaOver;
        public double cuotaUnder;

        public MercadoDTO(double overUnder, double cuotaOver, double cuotaUnder)
        {
            this.overUnder = overUnder;
            this.cuotaOver = cuotaOver;
            this.cuotaUnder = cuotaUnder;
        }

        public MercadoDTO()
        {
        }
    }
    #endregion

 
}