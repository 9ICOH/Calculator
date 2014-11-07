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
        public CalculatorDatabase() : base("CalculatorConnection") { }
        public DbSet<Operation> Operations { get; set; }
        
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Operation>().HasMany(o=>o.Id)
        //    base.OnModelCreating(modelBuilder);
        //}


    }
}