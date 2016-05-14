using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GestionLegalite.Models;

namespace GestionLegalite.Controllers
{
    public class asesoresController : Controller
    {
        private legaliteEntities2 db = new legaliteEntities2();

        // GET: asesores
        public ActionResult Index()
        {
            return View(db.asesores.ToList());
        }

        // GET: asesores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asesore asesore = db.asesores.Find(id);
            if (asesore == null)
            {
                return HttpNotFound();
            }
            return View(asesore);
        }

        // GET: asesores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: asesores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idasesor,nombreusuario,password,nivel,nombre,salariobasico,costodiario,costohora")] asesore asesore)
        {
            if (ModelState.IsValid)
            {
                db.asesores.Add(asesore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asesore);
        }

        // GET: asesores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asesore asesore = db.asesores.Find(id);
            if (asesore == null)
            {
                return HttpNotFound();
            }
            return View(asesore);
        }

        // POST: asesores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idasesor,nombreusuario,password,nivel,nombre,salariobasico,costodiario,costohora")] asesore asesore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asesore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asesore);
        }

        // GET: asesores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asesore asesore = db.asesores.Find(id);
            if (asesore == null)
            {
                return HttpNotFound();
            }
            return View(asesore);
        }

        // POST: asesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asesore asesore = db.asesores.Find(id);
            db.asesores.Remove(asesore);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult logIn()
        {
            return View();
        }

        /// <summary>
        /// Verificar los datos sumunistrados por el usuario al realizar la peticion Post de envio de información 
        /// mediante la GUI de Autenticación de la aplicación. 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult logIn(asesore username)
        {
            if (ModelState.IsValid) //Verificar que el modelo de datos sea valido en cuanto a la definición de las propiedades
            {
                if (Isvalid(username.nombreusuario, username.password))//Verificar que el email y clave exista utilizando el método privado 
                {

                    FormsAuthentication.SetAuthCookie(username.nombreusuario, false); //crea variable de usuario
                    
                    return RedirectToAction("index", "asesores");  //dirigir controlador home vista Index una vez se a autenticado en el sistema
                }
                else
                {
                    ModelState.AddModelError("", "Algo fallo"); //adicionar mensaje de error al model 
                }
            }
            return View(username);
        }

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("login", "asesores");
        }

        /// <summary>
        /// Metodo para validar el email y password del usuario, realiza la consulta a la bd
        /// </summary>
        /// <param name="Email">Email ingresado</param>
        /// <param name="password">Password ingresado</param>
        /// <returns>
        /// True:Usuario valido
        /// False Usuario Invalido
        /// </returns>
        private bool Isvalid(string username, string password)
        {
            bool Isvalid = false;
            using (var db = new legaliteEntities2())
            {
                var user = db.asesores.FirstOrDefault(u => u.nombreusuario == username); //consultar el primer registro con los el email del usuario
                if (user != null)
                {
                    if (user.password == password)  //Verificar password del usuario
                    {
                        Isvalid = true;
                    }
                }
            }
            return Isvalid;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
