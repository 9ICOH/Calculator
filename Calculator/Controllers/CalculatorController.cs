using Calculator.Data;
using Calculator.Models;
using Calculator.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;

    public class CalculatorController : Controller
    {
        private const string ModelName = "resultVm";

        private readonly IContext dataContext;

        private readonly IOperationService opService;

        private ResultViewModel resultVm;

        public CalculatorController(IContext dataContext, IOperationService operationService)
        {

            this.dataContext = dataContext;
            this.opService = operationService;

            if (this.Session == null)
            {
                this.resultVm = new ResultViewModel
                {
                    OutputLine = "0",
                    LastExpression = "0",
                    OperationsHistory = this.opService.All()
                };
            }
        }

        public ActionResult Calculator()
        {
            return this.View(this.resultVm);
        }

        [HttpPost]
        public string GetResult(string expression)
        {

            try
            {
                var result = "0";
                var calcMethod = this.opService.Compile<Calculate>(expression) as Calculate;
                if (calcMethod != null)
                {
                    result = calcMethod();
                }

                this.opService.Create(expression, result);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public ActionResult GetOperations()
        {
            return this.PartialView("HistoryOperationsView", this.opService.All());
        }

        protected override void Dispose(bool disposing)
        {
            if (this.dataContext != null)
            {
                this.dataContext.Dispose();
            }

            base.Dispose(disposing);
        }


    }
}
