using Ninject;
using SportsStore.Domain.Abtract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SportsStore.WebUI.Infracstructure
{
    public class KernelDependencyResolver : IDependencyResolver
    {
        IKernel kernel;
        public KernelDependencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            Addbinding();
        }

        private void Addbinding()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<IProcessOrder>().To<EmailOrderProcessor>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
        }

        public object GetService(Type serviceType)
        {
           return kernel.Get(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}