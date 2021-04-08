using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFConnection
{
    public interface IContext: IDisposable
    {
        DbSet<T> Repository<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesSaveChangesAsync();
    }
}
