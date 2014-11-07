using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Calculator.Data
{
    public class OperationRepository : EfRepository<Operation>, IOperationRepository
    {
        public OperationRepository(DbContext context, bool shredContext) : base(context, shredContext) { }

        public Operation GetBy(int id)
        {
            return Find(o => o.Id == id);
        }

        public Operation GetBy(string result)
        {
            return Find(o => o.Result == result);
        }
    }
}