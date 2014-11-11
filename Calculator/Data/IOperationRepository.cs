using Calculator.Models;
using System.Linq;

namespace Calculator.Data
{
    public interface IOperationRepository : IRepository<Operation>
    {
        Operation GetBy(int id);
        Operation GetBy(string operatr);
        IQueryable<Operation> GetAll();

    }
}