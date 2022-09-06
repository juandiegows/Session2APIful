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
using System.Web.Routing;
using Session2APIful.Models;
using Session2APIful.Models.Request;

namespace Session2APIful.Controllers
{
    public class UsuariosController : ApiController
    {
        private Sessao2Entities db = new Sessao2Entities();

        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuario()
        {
            return db.Usuario;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
        [Route("Api/auth")]
        public IHttpActionResult Login([FromBody] LoginRequest login)
        {
            Usuario usuario = db.Usuario.Where(x => (x.Nome.ToLower() == login.User.ToLower() || x.Email.ToLower() == login.User.ToLower()) && x.Senha == login.Password).FirstOrDefault();
            Usuario usuario2 = db.Usuario.Where(x => (x.Nome.ToLower() == login.User.ToLower() || x.Email.ToLower() == login.User.ToLower()) ).FirstOrDefault();
            if(usuario2 == null)
            {
                return BadRequest();
            }
            if (usuario!= null && !usuario.Bloqueado)
            {
                return Ok(usuario);
            }
            else if( usuario== null)
            {
              return  Ok(usuario);
            }else
            {
                return Content(HttpStatusCode.Unauthorized, usuario);
            }


        }
        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuario.Add(usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuario.Count(e => e.Id == id) > 0;
        }
    }
}