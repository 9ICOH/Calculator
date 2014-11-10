using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Services
{
    public interface IOperationService
    {
        Operation GetBy(int id);

        IEnumerable<Operation> All();
        
    }
}