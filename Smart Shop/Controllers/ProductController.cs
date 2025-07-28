using Smart_Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_Shop.Controllers
{
    public class ProductController : Controller
    {
        Smart_ShopEntities1 db = new Smart_ShopEntities1();
        // GET: Product
        public ActionResult Index()
        {
            var Products = db.Products.ToList();
            return View(Products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(product);
        }
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(int Id, string submit)
        {
            var product = db.Products.Find(Id);
            if (submit == "Yes")
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if( submit == "No")
            {
                return RedirectToAction("Index");
            }  
            return View(product);
        }

    }
}