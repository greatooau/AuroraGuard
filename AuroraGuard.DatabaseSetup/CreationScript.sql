CREATE TABLE IF NOT EXISTS Credentials (
    Id TEXT NOT NULL PRIMARY KEY,
    AccessUser TEXT NOT NULL,
    AccessPassword BLOB NOT NULL,
    AppName TEXT NOT NULL,
    ImagePath TEXT,
    Notes TEXT,
    UpdatedAt TEXT NOT NULL,
    CreatedAt TEXT NOT NULL
);