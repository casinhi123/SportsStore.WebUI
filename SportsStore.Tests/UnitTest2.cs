using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using SportsStore.Domain.Abtract;
using SportsStore.Domain;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Edit()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]
            {
                new Product{ProductID=1,Name="Jeans"},
                new Product{ProductID=2,Name="Jeans"}
            });
            AdminController target = new AdminController(mock.Object);
            //Action
            Product p1 = (Product)target.Edit(1).ViewData.Model;
            Product p2 = (Product)target.Edit(2).ViewData.Model;
            //Assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
        }
        [TestMethod]
        public void Can_Not_Edit_NonExist_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]
           {
                new Product{ProductID=1,Name="Jeans"},
                new Product{ProductID=2,Name="Jeans"}
           });
            AdminController target = new AdminController(mock.Object);
            //Action
            Product p3 = (Product)target.Edit(3).ViewData.Model;
            //Assert
            Assert.IsNull(p3);
        }

        public void Can_Save_Valid_Changes()
        {
            //Arrange-Create Mock Repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //Arrang-create the controller
            AdminController target = new AdminController(mock.Object);
            //Arrange-Create Product
            Product product = new Product { Name = "Test" };
            //Action- Try to save the product
            ActionResult result = target.Edit(product);
            //Assert- Check that  the repository was called
            mock.Verify(m => m.SaveProduct(product));
            //Assert- check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
    }
}
