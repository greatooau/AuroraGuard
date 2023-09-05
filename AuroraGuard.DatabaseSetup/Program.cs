using AuroraGuard.IoC;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace AuroraGuard.DatabaseSetup;

public static class Program
{
	public static void Main(string[] args)
	{
		var configuration = AuroraGuardConfiguration.Get();
		var dbName = configuration.GetConnectionString("aurora-guard");

		if (dbName is null) throw new Exception("Connection string has not been set already");

		var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dbName);

		if (File.Exists(filePath))
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			
			Console.WriteLine("Database already exists!");
			Console.WriteLine(filePath);
			
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Do you want to delete it? Y/N");
			Console.ResetColor();

			var options = new[] {'Y', 'N'};

			while (true)
			{
				var key = Console.ReadKey();
				
				if (!options.Contains(key.KeyChar))
				{
					Thread.Sleep(200);
					// Get the current cursor position
					var left = Console.CursorLeft;
					var top = Console.CursorTop;

					// Move the cursor to the beginning of the input line
					Console.SetCursorPosition(left - 1, top);

					// Write a space to delete the last character
					Console.Write(" ");

					// Move the cursor back to the beginning of the input line
					Console.SetCursorPosition(left - 1, top);
					continue;
				}

				if (key.KeyChar == 'Y')
				{
					try
					{
						File.Delete(filePath);
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("\nDatabase deleted succesfully!");
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						throw;
					}
				}

				break;
			}

			return;
		}

		var dirPath = Path.GetDirectoryName(filePath)!;
		Directory.CreateDirectory(dirPath);
		File.Create(filePath).Close();

		using var connection = new SqliteConnection($"DataSource={filePath};");

		connection.Open();

		string sql;
		try
		{
			sql = File.ReadAllText("CreationScript.sql");
		}
		catch (FileNotFoundException e)
		{
			Console.WriteLine(e.ToString());
			return;
		}

		try
		{
			using var command = new SqliteCommand(sql, (SqliteConnection?) connection);

			command.ExecuteNonQuery();
		}
		catch (SqliteException e)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Database couldn't be created");
			Console.WriteLine(e.ToString());
			return;
		}
		finally
		{
			connection.Close();
		}
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Database created successfully");
	}
}