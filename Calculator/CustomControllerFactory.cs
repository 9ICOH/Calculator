using System;

namespace Calculator
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using Calculator.Controllers;
    using Calculator.Data;
    using Calculator.Services;

    using Ninject;

    public class CustomControllerFactory : IControllerFactory
    {
        [Inject]
        public IContext Context { get; set; }

        [Inject]
        public IOperationService OpService { get; set; }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = new CalculatorController();
            return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}