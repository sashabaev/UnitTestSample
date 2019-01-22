using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class ATMNotEnoughMoneyException : Exception
    {
        public ATMNotEnoughMoneyException(string message) : base(message)
        {
        }
    }
}
