using Calculator.Models;
using System;
using System.Collections.Generic;

namespace Calculator.Services
{
    public interface IOperationService
    {
        Operation GetBy(int id);

        IEnumerable<Operation> All();

        Delegate Compile<T>(string code);

        Operation Create(string exprs, string res);
    }
}