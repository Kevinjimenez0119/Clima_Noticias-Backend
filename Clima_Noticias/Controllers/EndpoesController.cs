using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Clima_Noticias.Models;
using System.Web.Http.Cors;


namespace Clima_Noticias.Controllers
{
    [EnableCors(origins: "*", headers:"*", methods:"*")]
    public class EndpoesController : ApiController
    {
        private climaEntities db = new climaEntities();

        ApisController api = new ApisController();

        // GET: api/Endpoes
        public IQueryable<Endpo> GetEndpo()
        {
            return db.Endpo;
        }

        // GET: api/Noticias/pais
        // metodo get obtiene las noticias de una ciudad o una palabra en especifico
        [ResponseType(typeof(Endpo))]
        public IHttpActionResult GetNoticia(String query)
        {
            Endpo endpo = db.Endpo.Find(1);
            if (endpo == null)
            {
                return NotFound();
            }

            if (query == "")
            {
                return NotFound();
            }

            try
            {
                dynamic respuesta = api.Get(endpo.endpoin + query + "&apiKey=" + endpo.apikey);
                return Ok(respuesta.articles);
            }
            catch (Exception ex)
            {

                return NotFound();

                
            }
            
        }
        // GET: api/Clima/pais
        // metodo get obtiene el clima de una ciudad
        [ResponseType(typeof(Endpo))]
        public IHttpActionResult GetClima(String ciudad)
        {
            Endpo endpo = db.Endpo.Find(2);
            if (endpo == null)
            {
                return NotFound();
            }
            if (ciudad == "")
            {
                return NotFound();
            }
            try
            {
                dynamic respuesta = api.Get(endpo.endpoin + ciudad + "&APPID=" + endpo.apikey);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {

                return Ok("No es una ciudad");
            }


        }


        // GET: api/Endpoes/5
        [ResponseType(typeof(Endpo))]
        public IHttpActionResult GetEndpo(int id)
        {
            Endpo endpo = db.Endpo.Find(id);
            if (endpo == null)
            {
                return NotFound();
            }

            return Ok(endpo);
        }

        // PUT: api/Endpoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEndpo(int id, Endpo endpo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != endpo.idend)
            {
                return BadRequest();
            }

            db.Entry(endpo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndpoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Endpoes
        [ResponseType(typeof(Endpo))]
        public IHttpActionResult PostEndpo(Endpo endpo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Endpo.Add(endpo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = endpo.idend }, endpo);
        }

        // DELETE: api/Endpoes/5
        [ResponseType(typeof(Endpo))]
        public IHttpActionResult DeleteEndpo(int id)
        {
            Endpo endpo = db.Endpo.Find(id);
            if (endpo == null)
            {
                return NotFound();
            }

            db.Endpo.Remove(endpo);
            db.SaveChanges();

            return Ok(endpo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EndpoExists(int id)
        {
            return db.Endpo.Count(e => e.idend == id) > 0;
        }
    }
}