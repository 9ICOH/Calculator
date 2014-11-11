using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Calculator.Data
{
    public class CalculatorDatabase : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
    }
}