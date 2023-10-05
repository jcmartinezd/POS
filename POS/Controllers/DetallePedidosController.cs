﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POS.Models;

namespace POS.Controllers
{
    public class DetallePedidosController : Controller
    {
        private EjemploTiendaEntities db = new EjemploTiendaEntities();

        // GET: DetallePedidos
        public ActionResult Index()
        {
            var detallePedidos = db.DetallePedidos.Include(d => d.Pedidos).Include(d => d.Productos);
            return View(detallePedidos.ToList());
        }

        // GET: DetallePedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedidos detallePedidos = db.DetallePedidos.Find(id);
            if (detallePedidos == null)
            {
                return HttpNotFound();
            }
            return View(detallePedidos);
        }

        // GET: DetallePedidos/Create
        public ActionResult Create()
        {
            ViewBag.id_pedido = new SelectList(db.Pedidos, "id_pedido", "id_pedido");
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "nombre");
            return View();
        }

        // POST: DetallePedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pedido,id_producto,cantidad")] DetallePedidos detallePedidos)
        {
            if (ModelState.IsValid)
            {
                db.DetallePedidos.Add(detallePedidos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pedido = new SelectList(db.Pedidos, "id_pedido", "id_pedido", detallePedidos.id_pedido);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "nombre", detallePedidos.id_producto);
            return View(detallePedidos);
        }

        // GET: DetallePedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedidos detallePedidos = db.DetallePedidos.Find(id);
            if (detallePedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pedido = new SelectList(db.Pedidos, "id_pedido", "id_pedido", detallePedidos.id_pedido);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "nombre", detallePedidos.id_producto);
            return View(detallePedidos);
        }

        // POST: DetallePedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pedido,id_producto,cantidad")] DetallePedidos detallePedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pedido = new SelectList(db.Pedidos, "id_pedido", "id_pedido", detallePedidos.id_pedido);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "nombre", detallePedidos.id_producto);
            return View(detallePedidos);
        }

        // GET: DetallePedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedidos detallePedidos = db.DetallePedidos.Find(id);
            if (detallePedidos == null)
            {
                return HttpNotFound();
            }
            return View(detallePedidos);
        }

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePedidos detallePedidos = db.DetallePedidos.Find(id);
            db.DetallePedidos.Remove(detallePedidos);
            db.SaveChanges();
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
