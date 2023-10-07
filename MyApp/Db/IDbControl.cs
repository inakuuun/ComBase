using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Db
{
    public interface IDbControl : IDisposable
    {
        void Open();

        void ExecuteNonQuery(string str);

        void TransactionStart();

        void TransactionCommit();

        void TransactionRollback();
    }
}
