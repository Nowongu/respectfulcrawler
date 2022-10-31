-- SQLite
CREATE TABLE IF NOT EXISTS document (
	id INTEGER PRIMARY KEY,
   	url TEXT NOT NULL,
    last_updated TEXT NULL, --as ISO8601 strings ("YYYY-MM-DD HH:MM:SS.SSS").
	status TEXT NULL,
	body BLOB  NULL,
	content_type TEXT NULL,
	blob_download_ms INTEGER NULL
);
CREATE INDEX IF NOT EXISTS url_index ON document(url);