using System.Data.SQLite;

namespace Crawler.Lib;

public class DocumentRepository : RepositoryBase<Document>, IRepository<Document>
{
    public DocumentRepository()
        : base(Settings.Instance.ConnectionString ?? throw new ArgumentNullException("ConnectionString value is null"))
    { }

    protected override string TableName => "document";

    public override async Task<Document?> GetById(long id)
    {
        using var con = new SQLiteConnection(ConnectionString);
        using var cmd = new SQLiteCommand($"select * from document where @id = {id}", con);
        cmd.Parameters.AddWithValue("@id", id);
        await con.OpenAsync();

        var reader = await cmd.ExecuteReaderAsync();
        if (!reader.HasRows) return null;

        await reader.ReadAsync();

        return new Document()
        {
            Id = reader.GetInt32(0),
            Url = reader.GetValue(1)?.ToString(),
            LastUpdated = reader.GetDateTime(2),
            Status = reader.GetValue(3)?.ToString(),
            Body = reader.GetString(4),
            ContentType = reader.GetString(5)
        };
    }

    protected override async Task<int> Update(Document data)
    {
        using var con = new SQLiteConnection(ConnectionString);
        using var cmd = new SQLiteCommand("update document set last_updated = @lastUpdated, status = @status, body = @body, content_type = @contentType where id = @id", con);
        cmd.Parameters.AddWithValue("@id", data.Id);
        cmd.Parameters.AddWithValue("@url", data.Url);
        cmd.Parameters.AddWithValue("@lastUpdated", data.LastUpdated);
        cmd.Parameters.AddWithValue("@status", data.Status);
        cmd.Parameters.AddWithValue("@body", data.Body);
        cmd.Parameters.AddWithValue("@contentType", data.ContentType);
        await con.OpenAsync();
        return await cmd.ExecuteNonQueryAsync();
    }

    protected override async Task<int> Insert(Document data)
    {
        using var con = new SQLiteConnection(ConnectionString);
        using var cmd = new SQLiteCommand("insert into document (url, last_updated, status, body, content_type) values (@url, @lastUpdated, @status, @body, @contentType)", con);
        cmd.Parameters.AddWithValue("@url", data.Url);
        cmd.Parameters.AddWithValue("@lastUpdated", data.LastUpdated);
        cmd.Parameters.AddWithValue("@status", data.Status);
        cmd.Parameters.AddWithValue("@body", data.Body);
        cmd.Parameters.AddWithValue("@contentType", data.ContentType);
        await con.OpenAsync();
        return await cmd.ExecuteNonQueryAsync();
    }
}