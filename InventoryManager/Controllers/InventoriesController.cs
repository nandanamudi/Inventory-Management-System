using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryManager.Models;
using System.Threading.Tasks;

namespace InventoryManager.Controllers
{

    //if we want to not show quantitywarninglevel, we would do that here and return a different view.

    public class InventoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inventories
        public ActionResult Index()
        {
            return View(db.Inventory.ToList());
        }


        [Authorize(Roles = "Admin")]
        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        [Authorize(Roles = "Admin")]
        // GET: Inventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "ID,SKU,ItemName,Quantity,QuantityWarningLevel,QuantityRefill,QuantityBehavior")]*/ Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventory.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventory);
        }

        [Authorize(Roles = "Admin")]
        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "ID,SKU,Quantity,QuantityWarningLevel,QuantityRefill,QuantityBehavior")]*/ Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
            if (inventory.Quantity < inventory.QuantityWarningLevel)
            {
                MessageServices.SendEmail("inventory.manager@outlook.com", "Inventory Notification", "Your inventory is low.");
            }
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        [Authorize(Roles = "Admin")]
        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventory.Find(id);
            db.Inventory.Remove(inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string searchSKU)
        {
            //add try catch
            try {
                var SKU = (from Stock in db.Inventory
                           select Stock).ToList();
                if (!String.IsNullOrEmpty(searchSKU))
                {
                    SKU = SKU.Where(s => s.SKU.Equals(Convert.ToInt32(searchSKU))).ToList();
                }
                return View("Index", SKU);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //create a method that returns the google map dot of the correct location based on the SKU or do this in the method above


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
