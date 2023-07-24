using System.Data;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace AuroraGuard.DataAccess;

internal static class DatabaseFactory
{
	internal static IDbConnection CreateDatabaseConnection(string dbName)
	{
		if (dbName is null)
			throw new Exception("Connection string must not be null");

		var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dbName);
		if (!File.Exists(fileName))
			SQLiteConnection.CreateFile(fileName);

		IDbConnection connection = new SqliteConnection($"DataSource={fileName};");
		connection.Open();

		const string sql = @"
			CREATE TABLE IF NOT EXISTS User (
				Id TEXT NOT NULL PRIMARY KEY,
				Password TEXT NOT NULL,
				Name TEXT NOT NULL,
				Username TEXT NOT NULL,
				Salt TEXT NOT NULL,
				UpdatedAt TEXT NOT NULL,
				CreatedAt TEXT NOT NULL
			);

			CREATE TABLE IF NOT EXISTS Credential (
				Id TEXT NOT NULL PRIMARY KEY,
				AccessUser TEXT NOT NULL,
				AccessPassword TEXT NOT NULL,
				UserId TEXT NOT NULL,
				UpdatedAt TEXT NOT NULL,
				CreatedAt TEXT NOT NULL,
				FOREIGN KEY (UserId) REFERENCES User (Id)
			);
			";
		
		using var command = new SqliteCommand(sql, (SqliteConnection?)connection);

		command.ExecuteNonQuery();
		
		connection.Close();

		return connection;
	}
}