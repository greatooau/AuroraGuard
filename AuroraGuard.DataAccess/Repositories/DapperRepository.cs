using System.Data;
using AuroraGuard.Core.Interfaces.Repositories;
using Dapper;

namespace AuroraGuard.DataAccess.Repositories;

public class DapperRepository(IDbConnection dbConnection) : IDapperRepository
{
	public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                                          CommandType? commandType = null) =>
        dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);

    public Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object param = null!, IDbTransaction transaction = null!,
	                                             int? commandTimeout = null, CommandType? commandType = null) =>
        dbConnection.QuerySingleOrDefaultAsync<T?>(sql, param, transaction, commandTimeout, commandType);

    public Task<T> QuerySingleAsync<T>(string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                                   CommandType? commandType = null) =>
        dbConnection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);

    public Task<int> ExecuteAsync(string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                              CommandType? commandType = null) =>
        dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
}