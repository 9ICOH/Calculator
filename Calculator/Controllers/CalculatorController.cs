using Calculator.Data;
using Calculator.Models;
using Calculator.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    using System.Collections.Generic;

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

        public ActionResult SetSymbol(string submitButton)
        {
            if (this.Session[ModelName] != null)
            {
                this.resultVm = (ResultViewModel)this.Session[ModelName];
            }

            switch (submitButton)
            {
                case "(":
                case ")":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                case "+":
                case "-":
                case "*":
                case "/":
                case ".":
                    if (this.resultVm.OutputLine == "Error" | this.resultVm.OutputLine == "0" | this.resultVm.OutputLine == this.resultVm.LastCountedResult)
                    {
                        this.resultVm.OutputLine = string.Empty;
                    }

                    this.resultVm.OutputLine += submitButton;
                    this.resultVm.LastExpression = this.resultVm.OutputLine;
                    this.Session[ModelName] = this.resultVm;
                    return this.View("Calculator", this.resultVm);
                case "=":
                    try
                    {
                        var calcMethod = this.opService.Compile<Calculate>(this.resultVm.OutputLine) as Calculate;
                        if (calcMethod != null)
                        {
                            this.resultVm.LastCountedResult = calcMethod();
                        }

                        this.opService.Create(this.resultVm.OutputLine, this.resultVm.LastCountedResult);
                        this.resultVm.OutputLine = this.resultVm.LastCountedResult;
                        this.resultVm.OperationsHistory = this.opService.All();
                    }
                    catch (Exception ex)
                    {
                        this.resultVm.OutputLine = ex.Message;
                    }

                    return this.View("Calculator", this.resultVm);
                case "DEL":
                    var charArray = this.resultVm.OutputLine.ToArray();
                    var length = charArray.Length;

                    return this.View("Calculator", this.resultVm);
                case "AC":
                    this.resultVm = new ResultViewModel
                    {
                        OutputLine = "0",
                        LastExpression = "0"
                    };
                    Session.RemoveAll();
                    return this.View("Calculator", this.resultVm);
                default:
                    return this.View("Calculator", this.resultVm);
            }
        }

        public IEnumerable<Operation> GetOperations()
        {
            return this.opService.All();
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
