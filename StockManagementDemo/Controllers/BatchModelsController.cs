using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockManagementDemo.Models;

namespace StockManagementDemo.Controllers
{
    public class BatchModelsController : Controller
    {
        private BatchDBContext db = new BatchDBContext();

        // GET: BatchModels
        public ActionResult Index()
        {
            return View(db.BatchModels.ToList());
        }

        // GET: BatchModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchModel batchModel = db.BatchModels.Find(id);
            if (batchModel == null)
            {
                return HttpNotFound();
            }
            return View(batchModel);
        }

        // GET: BatchModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BatchModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchID,Fruit,Variety,Quantity")] BatchModel batchModel)
        {
            if (ModelState.IsValid)
            {
                db.BatchModels.Add(batchModel);                
                db.SaveChanges();
                UpdateStock(batchModel);
                return RedirectToAction("Index");
            }

            return View(batchModel);
        }

        // GET: BatchModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchModel batchModel = db.BatchModels.Find(id);
            if (batchModel == null)
            {
                return HttpNotFound();
            }
            return View(batchModel);
        }

        // POST: BatchModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchID,Fruit,Variety,Quantity")] BatchModel batchModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batchModel).State = EntityState.Modified;
                UpdateStock(batchModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(batchModel);
        }

        // GET: BatchModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchModel batchModel = db.BatchModels.Find(id);
            if (batchModel == null)
            {
                return HttpNotFound();
            }
            return View(batchModel);
        }

        // POST: BatchModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BatchModel batchModel = db.BatchModels.Find(id);
            db.BatchModels.Remove(batchModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void UpdateStock(BatchModel batch)
        {
            var stock = db.stocks.Where(x => x.Fruit.ToLower() == batch.Fruit.ToLower() && x.Variety.ToLower() == batch.Variety.ToLower()).FirstOrDefault();
            if (stock == null)
            {
                db.stocks.Add(new Stock { Fruit = batch.Fruit, Variety = batch.Variety, Quantity = batch.Quantity });
            }
            else
            {
                stock.Quantity = 0;
                foreach (BatchModel batchModel in db.BatchModels.Where(x => x.Fruit.ToLower() == batch.Fruit.ToLower() && x.Variety.ToLower() == batch.Variety.ToLower()))
                {
                    stock.Quantity += batchModel.Quantity;
                }
            }
            db.SaveChanges();


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
