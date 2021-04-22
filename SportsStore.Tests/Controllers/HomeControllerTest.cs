using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Abtract;
using SportsStore.Domain;
using SportsStore.WebUI.Models;

namespace SportsStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Indicaties_Selected_Categories()
        {
            //Arrange- create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new
            Product[] {
                new Product {ProductID = 1, Name ="P1", Category = "Apples"},
                new Product {ProductID = 4, Name ="P2", Category = "Oranges"},
            });
            //Arrang- create the controller
            NavController target = new NavController(mock.Object);
            //Arrange- define the category to selectd
            string categories = "Apples";
            //Action
            string result = target.Menu(categories).ViewBag.SelectedCategory;
            //Assert
            Assert.AreEqual(categories, result);
        }
        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange mock Object
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]
            {
                new Product {ProductID = 2, Name ="P2", Category = "Cat2"},
                new Product {ProductID = 3, Name ="P3", Category = "Cat1"},
                new Product {ProductID = 4, Name ="P4", Category = "Cat2"},
                new Product {ProductID = 5, Name ="P5", Category = "Cat3"}
            });
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;
            //action
            int res1 = ((ProductListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll= ((ProductListViewModel)target.List(null).Model).PagingInfo.TotalItems;
            //Assert
            Assert.AreEqual(1, res1);
            Assert.AreEqual(2, res2);
            Assert.AreEqual(1, res3);
            Assert.AreEqual(4, resAll);
        }
    }
}
