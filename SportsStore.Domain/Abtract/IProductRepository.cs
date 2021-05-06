using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Domain.Abtract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get;}
        void SaveProduct(Product product);
    }
}