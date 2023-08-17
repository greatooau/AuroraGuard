using System.Data;
using Dapper;

namespace AuroraGuard.DataAccess.Repositories.DapperRepository;

public class DapperRepository : IDapperRepository
{
	private readonly IDbConnection _dbConnection;

	public DapperRepository(IDbConnection dbConnection)
	{
		_dbConnection = dbConnection;
	}

	public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                                          CommandType? commandType = null)
	{
		return _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
	}

	public Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object param = null!, IDbTransaction transaction = null!,
	                                             int? commandTimeout = null, CommandType? commandType = null)
	{
		return _dbConnection.QuerySingleOrDefaultAsync<T?>(sql, param, transaction, commandTimeout, commandType);
	}

	public Task<T> QuerySingleAsync<T>(string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                                   CommandType? commandType = null)
	{
		return _dbConnection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);
	}

	public Task<int> ExecuteAsync(string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                              CommandType? commandType = null)
	{
		return _dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
	}
}