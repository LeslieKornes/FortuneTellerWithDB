using FortuneTellerWithDB.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FortuneTellerWithDB.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.FirstName = customer.FirstName;
            ViewBag.LastName = customer.LastName;
            #region AgeToRetirement
            if (customer.Age % 2 == 0)
                ViewBag.Retire = "45 long years";
            else
                ViewBag.Retire = "5 short and fun years";
            #endregion

            #region BirthMonthToBalance
            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
                ViewBag.Balance = 254;

            else if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
                ViewBag.Balance = 1000;

            else if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
                ViewBag.Balance = 50000;

            else
                ViewBag.Balance = 0;
            #endregion

#region FavoriteColorToTransportation
            switch (customer.FovoriteColor.ToLower())
            {
                case "red":
                    ViewBag.Transportation = "Jeep";
                    break;

                case "orange":
                    ViewBag.Transportation = "Subaru Outback";
                    break;

                case "yellow":
                    ViewBag.Transportation = "Ford Mustang";
                    break;

                case "green":
                    ViewBag.Transportation = "bicycle";
                    break;

                case "blue":
                    ViewBag.Transportation = "moped";
                    break;

                case "indigo":
                    ViewBag.Transportation = "skateboard";
                    break;

                case "violet":
                    ViewBag.Transportation = "Ford Focus";
                    break;
            }
            #endregion

            #region SiblingToVacation
            if (customer.SiblingCount == 0)
                ViewBag.Vacation = "Paris, France";

            else if (customer.SiblingCount == 1)
                ViewBag.Vacation = "Amsterdam";

            else if (customer.SiblingCount == 2)
                ViewBag.Vacation = "Geneva, Ohio";

            else if (customer.SiblingCount == 3)
                ViewBag.Vacation = "Los Angeles, California";

            else if (customer.SiblingCount >= 4)
                ViewBag.Vacation = "Capena, Italy";

            else
                ViewBag.Vacation = "Idaho";
            #endregion

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FovoriteColor,SiblingCount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FovoriteColor,SiblingCount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
