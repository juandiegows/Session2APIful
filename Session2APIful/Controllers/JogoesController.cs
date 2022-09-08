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
    public class JogoesController : ApiController
    {
        private Sessao2Entities db = new Sessao2Entities();

        // GET: api/Jogoes
        public IHttpActionResult GetJogo()
        {
            return Ok(db.Jogo.ToList().Select(x => new
            {
                x.Id,
                x.Data,
                Estadio = x.Estadio1.Nome,
                Home = x.Selecao.Nome,
                Visit = x.Selecao1.Nome,
                HomeB = x.Selecao.Bandeira,
                VisitB = x.Selecao1.Bandeira,
                GoalH = x.PlacarCasa,
                GoalV = x.PlacarVisitante   

            }).ToList());
        }

        // GET: api/Jogoes/5
        [ResponseType(typeof(Jogo))]
        public IHttpActionResult GetJogo(int id)
        {
            var jogo = db.Jogo.ToList().Select(x => new
            {
                x.Id,
                x.Data,
                Estadio = x.Estadio1.Nome,
                Home = x.Selecao.Nome,
                Visit = x.Selecao1.Nome,
                HomeB = x.Selecao.Bandeira,
                VisitB = x.Selecao1.Bandeira,
                GoalH = x.PlacarCasa,
                GoalV = x.PlacarVisitante

            }).FirstOrDefault(x => x.Id == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return Ok(jogo);
        }

        // PUT: api/Jogoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJogo(int id, Jogo jogo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jogo.Id)
            {
                return BadRequest();
            }

            db.Entry(jogo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogoExists(id))
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

        // POST: api/Jogoes
        [ResponseType(typeof(Jogo))]
        public IHttpActionResult PostJogo(Jogo jogo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jogo.Add(jogo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JogoExists(jogo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jogo.Id }, jogo);
        }

        // DELETE: api/Jogoes/5
        [ResponseType(typeof(Jogo))]
        public IHttpActionResult DeleteJogo(int id)
        {
            Jogo jogo = db.Jogo.Find(id);
            if (jogo == null)
            {
                return NotFound();
            }

            db.Jogo.Remove(jogo);
            db.SaveChanges();

            return Ok(jogo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JogoExists(int id)
        {
            return db.Jogo.Count(e => e.Id == id) > 0;
        }
    }
}