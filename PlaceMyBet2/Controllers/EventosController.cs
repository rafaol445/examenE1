﻿using Api_PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_PlaceMyBet.Controllers
{
    public class EventosController : ApiController
    {
        EventosRepository eventoRepository = new EventosRepository();

        // GET: api/Eventos
        public IEnumerable<EventoDto> Get()
        {
            
            return eventoRepository.RetriveDTO();


        }

        // GET: api/Eventos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Eventos
        public void Post([FromBody]EventoExamen evento)
        {
            eventoRepository.insertarEvento(evento);

        }

        // PUT: api/Eventos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Eventos/5
        public void Delete(int id)
        {
        }
    }
}
