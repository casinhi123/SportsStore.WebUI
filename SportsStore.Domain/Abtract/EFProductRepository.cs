using SportsStore.Domain.ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Domain.Abtract
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products { get { return context.Products; } }
    }
}