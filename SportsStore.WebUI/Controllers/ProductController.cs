using SportsStore.Domain.Abtract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository repository;
        int PageSize = 4;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        // GET: Product
        public ActionResult List(string category,int page=1)
        {
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = repository.Products.Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems =repository.Products.Count(),
                    ItemsPerpage = PageSize,
                    CurrentPage = page
                },
                CurrentCategories = category
            };
            return View(model);
        }
    }
}