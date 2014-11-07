using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Expression { get; set; }
        public string Result { get; set; }

    }
}