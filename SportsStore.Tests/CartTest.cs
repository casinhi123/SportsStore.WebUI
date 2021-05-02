using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain;
using SportsStore.Domain.Abtract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Tests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_Item_To_Cart()
        {
            // Arrange - create some test
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results =
            target.Lines.ToArray();
            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }
        [TestMethod]
        public void Can_Add_Quantity_Of_Existing_Lines()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();
            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            CartLine[] result = target.Lines.OrderBy(r => r.Product.ProductID).ToArray();
            //Assert
            Assert.AreEqual(4, result[0].Quantity);
            Assert.AreEqual(1, result[1].Quantity);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            target.AddItem(p3, 3);
            //CartLine[] result = target.Lines.OrderBy(p => p.Product.ProductID).ToArray();
            //Action
            target.RemoveLine(p1);
            target.RemoveLine(p3);
            //Assert
            Assert.AreEqual(0, target.Lines.Where(p => p.Product == p1).Count());
            Assert.AreEqual(0, target.Lines.Where(p => p.Product == p3).Count());
            Assert.AreEqual(1, target.Lines.Where(p => p.Product == p2).Count());
        }
        [TestMethod]
        public void Caculate_Cart_Tottal()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1",Price=2000 };
            Product p2 = new Product { ProductID = 2, Name = "P2" , Price = 3000 };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 4000 };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            target.AddItem(p3, 3);
            //Action
            decimal result = target.ComputeToTalValue();
            //Assert
            Assert.AreEqual(20000, result);
        }
        [TestMethod]
        public void Can_Clear_Cart()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 2000 };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 3000 };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 4000 };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            target.AddItem(p3, 3);
            //Action    
            target.Clear();
            CartLine[] result = target.Lines.ToArray();
            //Assert
            Assert.AreEqual(0, result.Length);
        }
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]
            {
                new Product{ProductID=1, Name="P1", Category="Apples"}
            }.AsQueryable());
            //Arrange- Create the Cart
            Cart cart = new Cart();
            //Arrange- Create a controller
            CartController target = new CartController(mock.Object,null);
            //Action-Add a product to a Cart
            target.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(),1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]
            {
                new Product{ProductID=1, Name="P1", Category="Apples"}
            }.AsQueryable());
            //
            //Arrange - Create controller
            CartController target = new CartController(mock.Object,null);
            //Arrage - Create new Cart
            Cart cart = new Cart();
            //Action
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");
            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }
        
        //public void Can_View_Cart_Contents()
        //{
        //    //Arrange- Create new Cart
        //    Cart cart = new Cart();
        //    //Arrange- Create CartController
        //    CartController target = new CartController(null);
        //    //Action- Call the Index action method
        //    CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
            
        //}
    }
}
