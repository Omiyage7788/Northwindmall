using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthWind.Models;
using PagedList;

namespace NorthWind.Controllers
{

    [CheckLoginState]
    public class CustomersController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        DataConnection dc = new DataConnection();

        // GET: Customers
        public ActionResult Index(int page=1)
        {

            

            var cust = db.Customers.ToList();

            var pageSize = 15;

            var currentPage = pageSize < 1 ? 1 : page;

            var pageCust = cust.ToPagedList(currentPage, pageSize);

            return View(pageCust);
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customers);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Customers customers)
        {

            ModelState.Remove("CustomerID");
            if (ModelState.IsValid)
            {
                string sql = "UPDATE Customers SET ";
                sql += " [CompanyName] = '" + customers.CompanyName;
                sql += " ' ,[ContactName] = '" + customers.ContactName;
                sql += " ' ,[ContactTitle] = '" + customers.ContactTitle;
                sql += " ' ,[Address] = '" + customers.Address;
                sql += " ' ,[City] = '" + customers.City;
                sql += "  ' ,[Region] = '" + customers.Region;
                sql += "  ',[PostalCode] = '" + customers.PostalCode;
                sql += " ' ,[Country] = '" + customers.Country;
                sql += " ' ,[Phone] = '" + customers.Phone;
                sql += " ' ,[Fax] = '" + customers.Fax;
                sql += "'  WHERE CustomerID = '" + customers.CustomerID + "'";



                dc.executeSql(sql);


                //db.Entry(customers).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customers);
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
