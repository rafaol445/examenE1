using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_PlaceMyBet.Models
{
    public class Evento
    {
        #region Atributos
        public int idEvento;
        public string local;
        public string visitante;
        public DateTime fecha;
        #endregion

        #region Constructores
        public Evento(int idEvento, string local, string visitante, DateTime fecha)
        {
            this.idEvento = idEvento;
            this.local = local;
            this.visitante = visitante;
            this.fecha = fecha;
        }

        public Evento()
        {
        }
        #endregion
    }
}
public class EventoDto {

        public string local;
        public string visitante;
        public DateTime fecha;



    public EventoDto(string local, string visitante, DateTime fecha)
    {
        this.local = local;
        this.visitante = visitante;
        this.fecha = fecha;

    }

    public EventoDto()
    {
    }




}