using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Data
{
    public interface IContext : IDisposable
    {
        IOperationRepository Operations { get; }

        int SaveChanges();
    }
}