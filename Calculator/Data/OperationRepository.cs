using Calculator.Models;
using System.Data.Entity;
using System.Linq;

namespace Calculator.Data
{
    public class OperationRepository : EfRepository<Operation>, IOperationRepository
    {
        public OperationRepository(DbContext context, bool shredContext = true)
            : base(context, shredContext)
        {
        }

        public Operation GetBy(int id)
        {
            return this.Find(o => o.Id == id);
        }

        public Operation GetBy(string result)
        {
            return this.Find(o => o.Result == result);
        }

        public IQueryable<Operation> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}