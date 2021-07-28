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
    public class busquedasController : ApiController
    {
        private noticias_climaEntities2 db = new noticias_climaEntities2();

        // GET: api/busquedas
        public IQueryable<busquedas> Getbusquedas()
        {
            return db.busquedas.OrderByDescending(r => r.idbusqueda);
        }

        // GET: api/busquedas/5
        [ResponseType(typeof(busquedas))]
        public IHttpActionResult Getbusquedas(int id)
        {
            busquedas busquedas = db.busquedas.Find(id);
            if (busquedas == null)
            {
                return NotFound();
            }

            return Ok(busquedas);
        }

        // PUT: api/busquedas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbusquedas(int id, busquedas busquedas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != busquedas.idbusqueda)
            {
                return BadRequest();
            }

            db.Entry(busquedas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!busquedasExists(id))
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

        // POST: api/busquedas
        [ResponseType(typeof(busquedas))]
        public IHttpActionResult Postbusquedas(busquedas busquedas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.busquedas.Add(busquedas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = busquedas.idbusqueda }, busquedas);
        }

        // DELETE: api/busquedas/5
        [ResponseType(typeof(busquedas))]
        public IHttpActionResult Deletebusquedas(int id)
        {
            busquedas busquedas = db.busquedas.Find(id);
            if (busquedas == null)
            {
                return NotFound();
            }

            db.busquedas.Remove(busquedas);
            db.SaveChanges();

            return Ok(busquedas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool busquedasExists(int id)
        {
            return db.busquedas.Count(e => e.idbusqueda == id) > 0;
        }
    }
}