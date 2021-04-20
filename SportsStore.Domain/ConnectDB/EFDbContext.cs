using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SportsStore.Domain.ConnectDB
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}