using System.Collections.Generic;

namespace Calculator.Models
{
    public class ResultViewModel
    {
        public string OutputLine { get; set; }

        public string LastExpression { get; set; }

        public string LastCountedResult { get; set; }

        public IEnumerable<Operation> OperationsHistory { get; set; }
    }
}