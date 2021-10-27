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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class climasController : ApiController
    {
        private climaEntities db = new climaEntities();

        // GET: api/climas
        public IQueryable<climas> Getclimas()
        {
            return db.climas;
        }

        // GET: api/climas/5
        [ResponseType(typeof(climas))]
        public IHttpActionResult Getclimas(int id)
        {
            climas climas = db.climas.Find(id);
            if (climas == null)
            {
                return NotFound();
            }

            return Ok(climas);
        }

        // PUT: api/climas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putclimas(int id, climas climas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != climas.idclima)
            {
                return BadRequest();
            }

            db.Entry(climas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!climasExists(id))
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

        // POST: api/climas
        [ResponseType(typeof(climas))]
        public IHttpActionResult Postclimas(climas climas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.climas.Add(climas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = climas.idclima }, climas);
        }

        // DELETE: api/climas/5
        [ResponseType(typeof(climas))]
        public IHttpActionResult Deleteclimas(int id)
        {
            climas climas = db.climas.Find(id);
            if (climas == null)
            {
                return NotFound();
            }

            db.climas.Remove(climas);
            db.SaveChanges();

            return Ok(climas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool climasExists(int id)
        {
            return db.climas.Count(e => e.idclima == id) > 0;
        }
    }
}