using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Models
{
    public class ResultViewModel
    {
        public string OutputLine { get; set; }
        public string LastExpression { get; set; }
        public string LastCountedResult { get; set; }
    }
}