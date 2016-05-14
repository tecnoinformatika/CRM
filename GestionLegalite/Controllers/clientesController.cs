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
    public class clientesController : Controller
    {
        private legaliteEntities2 db = new legaliteEntities2();

        // GET: clientes
        public ActionResult Index()
        {
            return View(db.clientes.ToList());
        }

        // GET: clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nit,nombreusuario,password,nombre,departamento,valor")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nit,nombreusuario,password,nombre,departamento,valor")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cliente cliente = db.clientes.Find(id);
            db.clientes.Remove(cliente);
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
        public ActionResult logIn(cliente username, string returnUrl)
        {
            if (ModelState.IsValid) //Verificar que el modelo de datos sea valido en cuanto a la definición de las propiedades
            {
                if (Isvalid(username.nombreusuario, username.password))//Verificar que el email y clave exista utilizando el método privado 
                {
                    
                    FormsAuthentication.SetAuthCookie(username.nombreusuario, false); //crea variable de usuario
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("index", "solicitudes1");  //dirigir controlador home vista Index una vez se a autenticado en el sistema
                }
                else
                {
                    ModelState.AddModelError("", "Algo fallo"); //adicionar mensaje de error al model 
                }
            }
            return View(username);
        }

        [HttpPost]
        public ActionResult Login2(cliente model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (Isvalid(model.nombreusuario, model.password)){
                ModelState.AddModelError("", "The username/password combination does not match");
                return View();
            }
            FormsAuthentication.SetAuthCookie(model.nombreusuario, true);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)){
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Default");
        }

        /// <summary>
        /// Cerrar sesion del usuario autenticado
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Default");
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
                var user = db.clientes.FirstOrDefault(u => u.nombreusuario == username); //consultar el primer registro con los el email del usuario
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
