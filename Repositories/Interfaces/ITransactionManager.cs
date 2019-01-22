using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITransactionManager
    {
        void Begin();

        void Commit();

        void RollBack();

        Task ExecuteInTransaction(Func<Task> action);
    }
}
