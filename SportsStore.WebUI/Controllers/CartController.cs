using SportsStore.Domain;
using SportsStore.Domain.Abtract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        IProductRepository repository;
        public CartController(IProductRepository repositoryParam)
        {
            this.repository = repositoryParam;
        }

        public ActionResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel {  ReturnUrl = returnUrl,Cart=cart });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int productID, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int productID, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult CheckOut()
        {
            return View(new ShippingDetails());
        }
        public PartialViewResult Sumary(Cart cart)
        {
            return PartialView(cart);
        }

        private Cart GetCar()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

       
    }
}