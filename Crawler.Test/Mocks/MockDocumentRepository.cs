using Crawler.Lib;
using System.Reflection.Metadata;

namespace Crawler.Test.Mocks
{
    internal class MockDocumentRepository : IRepository<Lib.Document>
    {
        public Dictionary<long, Lib.Document> Database = new Dictionary<long, Lib.Document>();
        long idTrack = 0;

        public Task<int> Delete(long id)
        {
            var success = Database.Remove(id);
            if (success) return Task.FromResult(1);
            return Task.FromResult(0);
        }

        public Task<int> DeleteAll()
        {
            var count = Database.Count();
            Database.Clear();
            return Task.FromResult(count);
        }

        public Task<Lib.Document?> GetById(long id)
        {
            var task = Task.FromResult<Lib.Document?>(null);
            if (Database.TryGetValue(id, out var result))
            {
                task = Task.FromResult<Lib.Document?>(result);
            }

            return task;
        }

        public Task<int> Save(Lib.Document cur)
        {
            if (cur.Id == 0) 
            {
                cur.Id = ++idTrack;
            }

            Database[cur.Id] = cur;
            return Task.FromResult(1);
        }
    }
}
