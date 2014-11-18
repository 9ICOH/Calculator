using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.App_Start
{
    using System.Configuration;
    using System.Data.Entity;
    using System.Web.Mvc;

    using Calculator.Data;
    using Calculator.Services;

    using Ninject;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver()
        {
            this.kernel = new StandardKernel();
            this.AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            this.kernel.Bind<CalculatorDatabase>().ToSelf().WithConstructorArgument(ConfigurationManager.ConnectionStrings[0].ConnectionString);
            this.kernel.Bind<IOperationRepository>().To<OperationRepository>();
            this.kernel.Bind<IContext>().To<Context>();
            this.kernel.Bind<IOperationService>().To<OperationService>();
        }
    }
}