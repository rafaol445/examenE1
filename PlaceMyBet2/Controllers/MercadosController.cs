using Api_PlaceMyBet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_PlaceMyBet.Controllers
{
    public class MercadosController : ApiController
    {
        MercadosRepository mercado = new MercadosRepository();
        

        // GET: api/Mercados
        public IEnumerable<MercadoDTO> Get()
        {
            return mercado.RetriveDTO();
        }

        // GET: api/Mercados/5
        public Mercado Get(int idEvento, double tipo)
        {
            Debug.WriteLine("hola mundo hola mundo");


            return mercado.MercadosPorEvento(idEvento, tipo);
        }

        // POST: api/Mercados
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mercados/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mercados/5
        public void Delete(int id)
        {
        }
    }
}
