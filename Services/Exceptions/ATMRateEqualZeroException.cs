using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class ATMRateEqualZeroException : Exception
    {
        public ATMRateEqualZeroException(string message) : base(message)
        {
        }
    }
}
