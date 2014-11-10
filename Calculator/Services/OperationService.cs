using Calculator.Data;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Services
{
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
            var operation = new Operation()
            {
                Expression = exprs,
                Result = res
            };
            this.operations.Create(operation);
            this.context.SaveChanges();
            return operation;
        }
    }
}