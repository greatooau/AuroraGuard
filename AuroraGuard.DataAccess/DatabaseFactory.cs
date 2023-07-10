using System.Data;
using Microsoft.Data.Sqlite;

namespace AuroraGuard.DataAccess;

internal static class DatabaseFactory
{
	internal static IDbConnection CreateDatabaseConnection(string connectionString)
	{
		if (connectionString is null)
			throw new Exception("Connection string must not be null");
		
		
		var connection = new SqliteConnection(connectionString);

		const string sql = @"
			CREATE TABLE IF NOT EXISTS User (
				Id TEXT NOT NULL PRIMARY KEY,
				Password TEXT NOT NULL,
				Name TEXT NOT NULL
			);

			CREATE TABLE IF NOT EXISTS Credentials (
				Id TEXT NOT NULL PRIMARY KEY,
				AccessUser TEXT NOT NULL,
				AccessPassword TEXT NOT NULL,
				UserId INTEGER NOT NULL,
				ModifiedAt TEXT NOT NULL,
				CreatedAt TEXT NOT NULL
			);
			";
		
		using var command = new SqliteCommand(sql, connection);

		command.ExecuteNonQuery();

		return connection;
	}
}