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
    public class actividadesController : Controller
    {
        private legaliteEntities2 db = new legaliteEntities2();

        // GET: actividades
        public async Task<ActionResult> Index()
        {
            return View(await db.actividades.ToListAsync());
        }

        // GET: actividades/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actividade actividade = await db.actividades.FindAsync(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            return View(actividade);
        }

        // GET: actividades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: actividades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "actividad,id")] actividade actividade)
        {
            if (ModelState.IsValid)
            {
                db.actividades.Add(actividade);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(actividade);
        }

        // GET: actividades/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actividade actividade = await db.actividades.FindAsync(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            return View(actividade);
        }

        // POST: actividades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "actividad,id")] actividade actividade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividade).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(actividade);
        }

        // GET: actividades/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actividade actividade = await db.actividades.FindAsync(id);
            if (actividade == null)
            {
                return HttpNotFound();
            }
            return View(actividade);
        }

        // POST: actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            actividade actividade = await db.actividades.FindAsync(id);
            db.actividades.Remove(actividade);
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
