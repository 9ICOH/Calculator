using System.Data.Entity;

namespace Calculator.Data
{
    public class Context : IContext
    {
        private readonly DbContext dbContext;

        public Context(CalculatorDatabase context, IOperationRepository operations = null)
        {
            this.dbContext = context;
            this.Operations = operations ?? new OperationRepository(this.dbContext);
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
               // try
              //  {
                    this.dbContext.Dispose();
              //  }
              //  catch { }
            }
        }
    }
}