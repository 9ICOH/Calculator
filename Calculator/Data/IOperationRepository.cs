﻿using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Data
{
    public interface IOperationRepository
    {
        Operation GetBy(int id);
        Operation GetBy(string operatr);
    }
}