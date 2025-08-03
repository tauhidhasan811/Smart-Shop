using AutoMapper;
using Smart_Shop.Database;
using Smart_Shop.Models;
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
        public Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            });

            return new Mapper(config);
        }

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
        public ActionResult AddCart(int id)
        {
            var data = db.Products.Find(id);
            
            var product = GetMapper().Map<ProductDTO>(data);
            List<ProductDTO> products = null;
            if (Session["cart"] == null)
            {
                products = new List<ProductDTO>();
            }
            else
            {
                products = (List<ProductDTO>)Session["cart"];
            }
            products.Add(product);
            Session["cart"] = products;
            return RedirectToAction("Index");
        }
        public ActionResult Cart()
        {
            var Product = Session["cart"];
            return View(Product);
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);

            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            product.Name = Request.Form["Name"];
            product.Price = Convert.ToDecimal(Request.Form["Price"]);
            product.Description = Request.Form["Description"];
            product.Quantity = Convert.ToDecimal(Request.Form["Quantity"]);

            db.SaveChanges();
            return RedirectToAction("Index");
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