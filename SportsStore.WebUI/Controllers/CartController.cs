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
        private IProductRepository repository;
        private IProcessOrder processOrder;
        public CartController(IProductRepository repositoryParam,IProcessOrder processOrder)
        {
            this.repository = repositoryParam;
            this.processOrder = processOrder;
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Xin lỗi, giỏ hàng của bạn đang trống!");
            }
            if (ModelState.IsValid)
            {
                processOrder.ProcessOrder(cart,shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
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