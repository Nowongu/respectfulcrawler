using System.Data.SQLite;

namespace Crawler.Lib
{
    public abstract class RepositoryBase<DataObject>
        where DataObject : IDataObject
    {
        protected abstract string TableName { get; }
        protected readonly string ConnectionString;

        public RepositoryBase(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public virtual async Task<int> Delete(long id)
        {
            using var con = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand($"delete from {TableName} where id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public virtual async Task<int> DeleteAll()
        {
            using var con = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand($"delete from {TableName};VACUUM", con);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> Save(DataObject cur)
        {
            if (cur.Id == 0)
                return await Insert(cur);
            return await Update(cur);
        }

        public abstract Task<DataObject?> GetById(long id);

        protected abstract Task<int> Update(DataObject data);

        protected abstract Task<int> Insert(DataObject data);
    }
}