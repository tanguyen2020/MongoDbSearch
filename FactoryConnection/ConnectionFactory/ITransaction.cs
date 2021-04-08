using System;
using System.Threading.Tasks;

namespace FactoryConnection.ConnectionFactory
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void RollBack();
    }
}
