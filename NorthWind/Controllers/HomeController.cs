using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthWind.Models;
using PagedList;

namespace NorthWind.Controllers
{
    public class HomeController : Controller
    {

        NorthwindEntities db = new NorthwindEntities();

        DataConnection dc = new DataConnection();
        // GET: Home
        public ActionResult Index(int page=1)
        {

            var products = db.Products.Where(p=>p.Discontinued==false).ToList();

           

            var pageSize = 20;

            var currentPage = pageSize < 1 ? 1 : page;

            var pageProducts = products.ToPagedList(currentPage, pageSize);

            return View(pageProducts);


            
        }

        [HttpPost]
        public ActionResult Index(string search,int page=1)
        {
            
            var productName = db.Products.Where(p => p.ProductName.Contains(search) && p.Discontinued == false).ToList();

            var pageSize = 20;

            var currentPage = pageSize < 1 ? 1 : page;

            var pageproductName = productName.ToPagedList(currentPage, pageSize);

            return View(pageproductName);

            
        }

        public ActionResult MyCart()
        {

            
            return View();
        }

        [CheckLoginState]
        public ActionResult Order()
        {
             Orders order = new Orders();


            order.OrderDate = DateTime.Today;

            ViewBag.ShipVia = new SelectList(db.Shippers,"ShipperID","CompanyName");


            return View(order);
        }

        [HttpPost]
        public ActionResult Order(Orders order,string cart)
        {
            order.RequiredDate = DateTime.Today.AddDays(20);

            if (order.ShipVia == 1)
                order.Freight = 50;
            else if (order.ShipVia == 2)
                order.Freight = 70;
            else if (order.ShipVia == 3)
                order.Freight = 90;

            

            
            dc.Cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            dc.Cmd.Parameters.AddWithValue("@Freight", order.Freight);
            dc.Cmd.Parameters.AddWithValue("@ShipVia", order.ShipVia);
            dc.Cmd.Parameters.AddWithValue("@ShipName", order.ShipName);
            dc.Cmd.Parameters.AddWithValue("@ShipAddress", order.ShipAddress);
            dc.Cmd.Parameters.AddWithValue("@ShipCity", order.ShipCity);
            dc.Cmd.Parameters.AddWithValue("@ShipPostalCode", order.ShipPostalCode);
            dc.Cmd.Parameters.AddWithValue("@ShipCountry", order.ShipCountry);
            dc.Cmd.Parameters.AddWithValue("@cart", cart);

            dc.executeProc("addOrders");

            TempData["Order"] = "Y";

            return RedirectToAction("MyCart");
        }

        public ActionResult creatMember() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult creatMember(Customers member)
        {

            if (ModelState.IsValid) 
            {
                db.Customers.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Account, string Password, string q)
        {
            var member = db.Customers.Where(m => m.CustomerID == Account && m.CustomerID == Password).FirstOrDefault();

            if (member == null)
            {
                ViewBag.msg = "帳號或密碼有錯!";
                return View();
            }

            var memberID=db.Customers.Find(member.CustomerID);

            Session["member"] = memberID.CustomerID;

            if(q=="1")
                return RedirectToAction("Order");

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session["member"] = null;
            return RedirectToAction("Login");
        }

        [CheckLoginState]
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
        public ActionResult Edit(Customers customers)
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

                return RedirectToAction("Index");
            }
            return View(customers);
        }


        [CheckLoginState]
        public ActionResult OrderList(string id)
        {          

            var orderID = db.Orders.Where(o => o.CustomerID == id).OrderByDescending(o => o.OrderDate).ToList();

            return View(orderID);
        }

        [CheckLoginState]
        public ActionResult OrderDetail(int OrderID)
        {

            var orderDetails = db.Order_Details.Where(o => o.OrderID == OrderID).ToList();


            return View(orderDetails);
        }

        
    }
}