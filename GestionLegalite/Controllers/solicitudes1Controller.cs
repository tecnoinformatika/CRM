using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionLegalite.Models;

namespace GestionLegalite.Controllers
{
    public class solicitudes1Controller : Controller
    {
        private legaliteEntities2 db = new legaliteEntities2();

        // GET: solicitudes1
        public async Task<ActionResult> Index()
        {
            var solicitudes = db.solicitudes.Include(s => s.asesore).Include(s => s.cliente).Include(s => s.actividade);
            return View(await solicitudes.ToListAsync());
        }

        // GET: solicitudes1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitude solicitude = await db.solicitudes.FindAsync(id);
            if (solicitude == null)
            {
                return HttpNotFound();
            }
            return View(solicitude);
        }

        // GET: solicitudes1/Create
        public ActionResult Create()
        {
            ViewBag.idasesor = new SelectList(db.asesores, "idasesor", "nombreusuario");
            ViewBag.nitcliente = new SelectList(db.clientes, "nit", "nombreusuario");
            ViewBag.actividad = new SelectList(db.actividades, "actividad", "actividad");
            return View();
        }

        // POST: solicitudes1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idsolicitud,nitcliente,idasesor,actividad,fechaHoraInicio,FechaHoraFin,estado,mensaje,Respuesta")] solicitude solicitude)
        {
            if (ModelState.IsValid)
            {
                db.solicitudes.Add(solicitude);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idasesor = new SelectList(db.asesores, "idasesor", "nombreusuario", solicitude.idasesor);
            ViewBag.nitcliente = new SelectList(db.clientes, "nit", "nombreusuario", solicitude.nitcliente);
            ViewBag.actividad = new SelectList(db.actividades, "actividad", "actividad", solicitude.actividad);
            return View(solicitude);
        }

        // GET: solicitudes1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitude solicitude = await db.solicitudes.FindAsync(id);
            if (solicitude == null)
            {
                return HttpNotFound();
            }
            ViewBag.idasesor = new SelectList(db.asesores, "idasesor", "nombreusuario", solicitude.idasesor);
            ViewBag.nitcliente = new SelectList(db.clientes, "nit", "nombreusuario", solicitude.nitcliente);
            ViewBag.actividad = new SelectList(db.actividades, "actividad", "actividad", solicitude.actividad);
            return View(solicitude);
        }

        // POST: solicitudes1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idsolicitud,nitcliente,idasesor,actividad,fechaHoraInicio,FechaHoraFin,estado,mensaje,Respuesta")] solicitude solicitude)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitude).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idasesor = new SelectList(db.asesores, "idasesor", "nombreusuario", solicitude.idasesor);
            ViewBag.nitcliente = new SelectList(db.clientes, "nit", "nombreusuario", solicitude.nitcliente);
            ViewBag.actividad = new SelectList(db.actividades, "actividad", "actividad", solicitude.actividad);
            return View(solicitude);
        }

        // GET: solicitudes1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solicitude solicitude = await db.solicitudes.FindAsync(id);
            if (solicitude == null)
            {
                return HttpNotFound();
            }
            return View(solicitude);
        }

        // POST: solicitudes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            solicitude solicitude = await db.solicitudes.FindAsync(id);
            db.solicitudes.Remove(solicitude);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
