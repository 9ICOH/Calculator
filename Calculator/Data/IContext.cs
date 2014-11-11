using System;

namespace Calculator.Data
{
    public interface IContext : IDisposable
    {
        IOperationRepository Operations { get; }

        int SaveChanges();
    }
}