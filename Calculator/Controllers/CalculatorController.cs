using Calculator.Data;
using Calculator.Models;
using Calculator.Services;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        private IContext dataContext;
        private OperationService opService;
        private const string ModelName = "resultVm";
        private ResultViewModel resultVm;

        public CalculatorController()
        {
            this.dataContext = new Context();
            this.opService = new OperationService(this.dataContext);

            if (Session == null)
            {
                this.resultVm = new ResultViewModel()
                {
                    OutputLine = "0",
                    LastExpression = "0",
                    OperationsHistory = this.opService.All()
                };
            }
        }

        public ActionResult Calculator()
        {
            return View(this.resultVm);
        }

        public ActionResult SetSymbol(string submitButton)
        {
            if (this.Session[ModelName] != null) this.resultVm = (ResultViewModel)this.Session[ModelName];

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
                    return View("Calculator", this.resultVm);
                case "=":
                    try
                    {
                        this.resultVm.LastCountedResult = (this.Compile<Calculate>(this.resultVm.OutputLine) as Calculate)();
                        this.opService.Create(this.resultVm.OutputLine, this.resultVm.LastCountedResult);
                        this.resultVm.OutputLine = this.resultVm.LastCountedResult;
                    }
                    catch (Exception ex)
                    {
                        this.resultVm.OutputLine = ex.Message;
                    }

                    return View("Calculator", this.resultVm);
                case "DEL":

                    return View("Calculator", this.resultVm);
                case "AC":
                    this.resultVm = new ResultViewModel()
                    {
                        OutputLine = "0",
                        LastExpression = "0"
                    };
                    Session.RemoveAll();
                    return View("Calculator", this.resultVm);
                default:
                    return View("Calculator", this.resultVm);
            }

        }

        private delegate string Calculate();

        private Delegate Compile<T>(string code)
        {
            string compileStr = string.Format(@"
            using System;
            class Calculator 
            {{
                public static string Invoke() 
                {{ 
                    return ((double){0}).ToString({1}); 
                }} 
            }}", code, '\u0022' + "0.00000" + '\u0022');

            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.GenerateInMemory = true;
            CompilerResults compileResults = new CSharpCodeProvider().CompileAssemblyFromSource(compilerParameters, compileStr);

            if (compileResults.Errors.Count > 0)
                throw new Exception("Error");
            else
                return Delegate.CreateDelegate(typeof(T), compileResults.CompiledAssembly.GetType("Calculator").GetMethod("Invoke"));
        }

        public ActionResult GetOperations()
        {
            var t = this.opService.All();
            this.resultVm.OperationsHistory = t;
            return View("Calculator", this.resultVm);
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
