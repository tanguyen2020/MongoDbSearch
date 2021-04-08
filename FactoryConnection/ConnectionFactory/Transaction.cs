using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace ADOConnection.ConnectionFactory
{
    public class Transaction : ITransaction
    {
        DbTransaction _transaction;
        public Transaction(DbTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Commit()
        {
            _transaction?.Commit();
        }

        public void RollBack()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        ~Transaction() => Dispose(false);
    }
}
