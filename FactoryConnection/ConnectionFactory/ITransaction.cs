using System;
using System.Threading.Tasks;

namespace ADOConnection.ConnectionFactory
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void RollBack();
    }
}
