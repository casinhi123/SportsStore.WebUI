using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Domain.Abtract
{
    public interface IProcessOrder
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}