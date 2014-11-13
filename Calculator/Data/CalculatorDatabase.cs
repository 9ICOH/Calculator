using Calculator.Models;
using System.Data.Entity;

namespace Calculator.Data
{
    public class CalculatorDatabase : DbContext
    {
        public CalculatorDatabase()
            : base("CalculatorConnection")
        {
        }

        public DbSet<Operation> Operations { get; set; }
    }
}