using Ninject;
using SetupValidator.Abstract;
using SetupValidator.Concrete;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SetupValidator.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernalParam)
        {
            kernel = kernalParam;
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<INormalRepository>().To<AdoNetNormalRepository>();
            //??? ????????????????? --> ?????????????????? ado or entity
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}