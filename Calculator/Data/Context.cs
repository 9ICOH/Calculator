﻿using System.Data.Entity;

namespace Calculator.Data
{
    public class Context : IContext
    {
        private readonly DbContext dbContext;

        public Context(DbContext context, IOperationRepository operations)
        {
            this.dbContext = context ?? new CalculatorDatabase();
            this.Operations = operations ?? new OperationRepository(this.dbContext, true);
        }

        public IOperationRepository Operations
        {
            get;
            private set;
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                try
                {
                    this.dbContext.Dispose();
                }
                catch { }
            }
        }
    }
}