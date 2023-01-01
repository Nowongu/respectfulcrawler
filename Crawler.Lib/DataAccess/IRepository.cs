namespace Crawler.Lib
{
    public interface IRepository<IDataObject>
    {
        Task<IDataObject?> GetById(long id);
        Task<int> Delete(long id);
        Task<int> DeleteAll();
        Task<int> Save(IDataObject cur);
    }
}