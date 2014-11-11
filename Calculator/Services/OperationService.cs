using Calculator.Data;
using Calculator.Models;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Services
{
    public delegate string Calculate();

    public class OperationService : IOperationService
    {
        private readonly IContext context;

        private readonly IOperationRepository operations;

        public OperationService(IContext context)
        {
            this.context = context;
            this.operations = context.Operations;
        }

        public IEnumerable<Operation> All()
        {
            return this.operations.GetAll().ToArray();
        }

        public Operation GetBy(int id)
        {
            return this.operations.GetBy(id);
        }

        public Operation Create(string exprs, string res)
        {
            var operation = new Operation
            {
                Expression = exprs,
                Result = res
            };
            this.operations.Create(operation);
            this.context.SaveChanges();
            return operation;
        }

        public Delegate Compile<T>(string code)
        {
            string compileStr = string.Format(@"
            using System;
            class Calculator 
            {{
                public static string Invoke() 
                {{ 
                    return ((double){0}).ToString({1}); 
                }} 
            }}", code, '\u0022' + "0.#####" + '\u0022');

            var compilerParameters = new CompilerParameters { GenerateInMemory = true };
            var compileResults = new CSharpCodeProvider().CompileAssemblyFromSource(compilerParameters, compileStr);

            if (compileResults.Errors.Count > 0)
            {
                throw new Exception("Error");
            }

            return Delegate.CreateDelegate(typeof(T), compileResults.CompiledAssembly.GetType("Calculator").GetMethod("Invoke"));
        }
    }
}