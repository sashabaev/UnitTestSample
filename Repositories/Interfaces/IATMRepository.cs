using Repositories.Models;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IATMRepository : IRepository<BankTransaction>
    {
       
    }
}
