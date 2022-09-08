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
using Session2APIful.Models;

namespace Session2APIful.Controllers
{
    public class SelecaosController : ApiController
    {
        private Sessao2Entities db = new Sessao2Entities();

        // GET: api/Selecaos
        public IHttpActionResult GetSelecao()
        {
            return Ok(db.Selecao.ToList().Select(x => new
            {
                x.Id,
                x.Bandeira,
                x.Nome,
                Point = Point(x.Id)

            }).ToList().OrderBy(x=> x.Nome).ThenBy(x=> x.Point).ToList());
        }

        public int Point(int id)
        {
            var seleccion = db.Selecao.Find(id);
            int cant = seleccion.Jogo.Count(x => x.PlacarVisitante == x.PlacarCasa);
            cant += seleccion.Jogo1.Count(x => x.PlacarVisitante == x.PlacarCasa);
            cant += seleccion.Jogo.Count(x => x.PlacarCasa > x.PlacarVisitante) * 3;
            cant += seleccion.Jogo1.Count(x => x.PlacarCasa < x.PlacarVisitante) * 3;

                return cant;
        }

        // GET: api/Selecaos/5
        public IHttpActionResult GetSelecao(int id)
        {
            var selecao = db.Selecao.ToList().Select(x => new
            {
                x.Id,
                x.Bandeira,
                x.Nome,
                Point = Point(x.Id)

            }).ToList().OrderBy(x => x.Nome).ThenBy(x => x.Point).FirstOrDefault(x=> x.Id == id);
            if (selecao == null)
            {
                return NotFound();
            }

            return Ok(selecao);
        }

        // PUT: api/Selecaos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSelecao(int id, Selecao selecao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selecao.Id)
            {
                return BadRequest();
            }

            db.Entry(selecao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelecaoExists(id))
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

        // POST: api/Selecaos
        [ResponseType(typeof(Selecao))]
        public IHttpActionResult PostSelecao(Selecao selecao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Selecao.Add(selecao);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = selecao.Id }, selecao);
        }

        // DELETE: api/Selecaos/5
        [ResponseType(typeof(Selecao))]
        public IHttpActionResult DeleteSelecao(int id)
        {
            Selecao selecao = db.Selecao.Find(id);
            if (selecao == null)
            {
                return NotFound();
            }

            db.Selecao.Remove(selecao);
            db.SaveChanges();

            return Ok(selecao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SelecaoExists(int id)
        {
            return db.Selecao.Count(e => e.Id == id) > 0;
        }
    }
}