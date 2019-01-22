using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories
{
    public class ATMRepository : RepositoryBase<BankTransaction>, IATMRepository
    {
        public ATMRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<BankTransaction> DbSet => DbContext.BankTransactions;
    }
}
