using Calculator.Models;
using System.Data.Entity;

namespace Calculator.Data
{
    public class CalculatorDatabase : DbContext
    {
        public CalculatorDatabase(string connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Operation> Operations { get; set; }
    }
}