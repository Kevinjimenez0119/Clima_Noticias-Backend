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
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class noticiasController : ApiController
    {
        private noticias_climaEntities2 db = new noticias_climaEntities2();

        // GET: api/noticias
        public IQueryable<noticias> Getnoticias()
        {
            return db.noticias;
        }

        // GET: api/noticias/5
        [ResponseType(typeof(noticias))]
        public IHttpActionResult Getnoticias(int id)
        {
            noticias noticias = db.noticias.Find(id);
            if (noticias == null)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        // PUT: api/noticias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putnoticias(int id, noticias noticias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noticias.idnoticia)
            {
                return BadRequest();
            }

            db.Entry(noticias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!noticiasExists(id))
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

        // POST: api/noticias
        [ResponseType(typeof(noticias))]
        public IHttpActionResult Postnoticias(noticias noticias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.noticias.Add(noticias);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = noticias.idnoticia }, noticias);
        }

        // DELETE: api/noticias/5
        [ResponseType(typeof(noticias))]
        public IHttpActionResult Deletenoticias(int id)
        {
            noticias noticias = db.noticias.Find(id);
            if (noticias == null)
            {
                return NotFound();
            }

            db.noticias.Remove(noticias);
            db.SaveChanges();

            return Ok(noticias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool noticiasExists(int id)
        {
            return db.noticias.Count(e => e.idnoticia == id) > 0;
        }
    }
}