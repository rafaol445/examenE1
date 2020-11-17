using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_PlaceMyBet.Models
{
    public class Apuesta
    {
        #region Atributos

        public int idApuesta;
        public string tipoCuota;
        public double cuota;
        public double dineroApostado;
        public DateTime fechaApuesta;
        public int idMercado;
        public string usuarioEmail;



        #endregion

        #region Constructores




        public Apuesta(int idApuesta, string tipoCuota, double cuota, double dineroApostado, DateTime fechaApuesta, int idMercado, string usuarioEmail)
        {
            this.idApuesta = idApuesta;
            this.tipoCuota = tipoCuota;
            this.cuota = cuota;
            this.dineroApostado = dineroApostado;
            this.fechaApuesta = fechaApuesta;
            this.idMercado = idMercado;
            this.usuarioEmail = usuarioEmail;
        }

        public Apuesta()
        {
        }

        #endregion
        #region Propiedades

        public string TipoCuota { get => tipoCuota; }
        public double DineroApostado { get => dineroApostado; }
        public DateTime FechaApuesta { get => fechaApuesta; }
        public int IdMercado { get => idMercado; }
        public string UsuarioEmail { get => usuarioEmail; }








        #endregion



    }
    public class ApuestaDTO
    {

        public String tipoCuota;
        public double cuota;
        public double dineroApostado;
        public DateTime fechaApuesta;
        public int idMercado;
        public string usuarioEmail;

        public ApuestaDTO(string tipoCuota, double cuota, double dineroApostado, DateTime fechaApuesta, int idMercado, string usuarioEmail)
        {
            this.tipoCuota = tipoCuota;
            this.cuota = cuota;
            this.dineroApostado = dineroApostado;
            this.fechaApuesta = fechaApuesta;
            this.idMercado = idMercado;
            this.usuarioEmail = usuarioEmail;
        }

        public ApuestaDTO()
        {
        }
    }



    #region Clase Apuesta Argumentos       

    public class ApuestaArgumentos
    {
        public double tipoMercado;
        public string tipoApuesta;
        public double cuota;
        public double dineroApostado;

        public ApuestaArgumentos(double tipoMercado, string tipoApuesta, double cuota, double dineroApostado)
        {
            this.tipoMercado = tipoMercado;
            this.tipoApuesta = tipoApuesta;
            this.cuota = cuota;
            this.dineroApostado = dineroApostado;
        }
        public ApuestaArgumentos()
        {
        }
    }
    

    public class ApuestasPorTipoMercado
    {

        public string tipoApuesta;
        public double cuota;
        public double dineroApostado;
        public int tipoEvento;

        public ApuestasPorTipoMercado(string tipoApuesta, double cuota, double dineroApostado, int tipoEvento)
        {
            this.tipoApuesta = tipoApuesta;
            this.cuota = cuota;
            this.dineroApostado = dineroApostado;
            this.tipoEvento = tipoEvento;
        }

        public ApuestasPorTipoMercado()
        {
        }
    }


    

    #endregion

    


    


}


