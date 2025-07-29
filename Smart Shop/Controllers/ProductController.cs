using Smart_Shop.Database;
using System;
using System.Collections.Generic;
using System.IO;
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
        /*
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                string fileName = System.IO.Path.GetFileName(Image.FileName);
                string path = Server.MapPath("~/Images/" + fileName);
                try
                {
                    Image.SaveAs(path);
                    product.Photo = fileName;
                    ViewBag.ImageError = "Image Saved";

                }
                catch (Exception ex)
                {
                    ViewBag.ImageError = "Image can not be saved." + ex.Message;
                    return View(product);

                }
                
            }
            else
            {
                product.Photo = "default.png"; 
            }
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase Image)
        {
            if (Image != null && Image.ContentLength > 0)
            {
                string fileName = Image.FileName;
                fileName = product.Id + "_" + product.Name + "_" + fileName;

                string path = Server.MapPath("~/App_Data/Images/" );
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path= Path.Combine(path, fileName);
                Image.SaveAs(path);
                product.Photo = fileName;
                ViewBag.ImageError = "Image Saved Successfully";
            }
            else
            {
                product.Photo = "noimage.jpg";
            }

            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*
        public ActionResult DisplayImage(string imageName)
        {
            string path = Server.MapPath("~App_Data/Images/");
            string contentType = MimeMapping.GetMimeMapping(path);
            return File(path, contentType);
        }
        */
        public ActionResult DisplayImage(string imageName)
        {
            string path = Server.MapPath("~/App_Data/Images/" + imageName);
            string contentType = MimeMapping.GetMimeMapping(path);
            return File(path, contentType);
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
        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }

    }
}