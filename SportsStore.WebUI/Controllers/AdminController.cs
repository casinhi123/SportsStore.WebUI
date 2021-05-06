using SportsStore.Domain;
using SportsStore.Domain.Abtract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Products);
        }
        public ViewResult Edit(int productID)
        {
            Product product = repository.Products.Where(p => p.ProductID == productID).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} đã được lưu lại", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
    }
}