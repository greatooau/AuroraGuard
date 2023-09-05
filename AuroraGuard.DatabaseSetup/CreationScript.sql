CREATE TABLE IF NOT EXISTS Credential (
    Id TEXT NOT NULL PRIMARY KEY,
    AccessUser TEXT NOT NULL,
    AccessPassword TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL,
    CreatedAt TEXT NOT NULL
);