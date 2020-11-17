using Api_PlaceMyBet.Models;
using PlaceMyBet2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaceMyBet2.Controllers
{
    public class ApuestasExamenController : ApiController
    {
        // GET: api/ApuestasExamen
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApuestasExamen/5
        public ApuestaExamen Get(string equipo)
        {
            ApuestasRepository apuestaRepo = new ApuestasRepository();

            return apuestaRepo.apuestasPorEquipo(equipo);

            
        }

        // POST: api/ApuestasExamen
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApuestasExamen/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApuestasExamen/5
        public void Delete(int id)
        {
        }
    }
}
