using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using BaseObject.DataObject;
using System.Linq;

namespace EFConnection
{
    public class BaseDbContext: DbContext
    {
        public BaseDbContext(DbContextOptions options): base(options)
        {

        }
        public virtual DbSet<T> Repository<T>() where T: class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<TDocument> QueryForObjectAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            DataObject dataObject = new DataObject();
            using (var command = base.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = executeStored ? CommandType.StoredProcedure : CommandType.Text;
                if (param != null)
                    command.Parameters.Add(ParseParameters(param));
                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    foreach(DataRow r in table.Rows)
                    {
                        foreach(DataColumn c in table.Columns)
                        {
                            dataObject.Add(c.ColumnName, r[c]);
                        }
                    }
                }
            }
            return (TDocument)Convert.ChangeType(dataObject, typeof(TDocument));
        }

        public async Task<List<TDocument>> QueryForListAsync<TDocument>(string sql, bool executeStored = false, IDictionary<string, object> param = null)
        {
            List<DataObject> dataObjects = new List<DataObject>();
            using (var command = base.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = executeStored ? CommandType.StoredProcedure : CommandType.Text;
                if (param != null)
                    command.Parameters.Add(ParseParameters(param));
                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    DataObject objData = new DataObject();
                    DataTable table = new DataTable();
                    table.Load(reader);
                    foreach (DataRow r in table.Rows)
                    {
                        foreach (DataColumn c in table.Columns)
                        {
                            objData.Add(c.ColumnName, r[c]);
                        }
                        dataObjects.Add(objData);
                    }
                }
            }
            return dataObjects.Cast<TDocument>().ToList();
        }

        public Dictionary<string, object> ParseParameters(IDictionary<string, object> param)
        {
            var parameters = new Dictionary<string, object>();
            foreach(var i in param)
            {
                parameters.Add($"@{i.Key}", i.Value ?? DBNull.Value);
            }
            return parameters;
        }
    }
}
