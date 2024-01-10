using System.Data;
using AuroraGuard.Core.Interfaces.Repositories;
using Dapper;

namespace AuroraGuard.DataAccess.Repositories;

public class DapperRepository : IDapperRepository
{
	public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection dbConnection, string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                                          CommandType? commandType = null) =>
        dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);

    public Task<T?> QuerySingleOrDefaultAsync<T>(IDbConnection dbConnection, string sql, object param = null!, IDbTransaction transaction = null!,
	                                             int? commandTimeout = null, CommandType? commandType = null) =>
        dbConnection.QuerySingleOrDefaultAsync<T?>(sql, param, transaction, commandTimeout, commandType);

    public Task<T> QuerySingleAsync<T>(IDbConnection dbConnection, string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                                   CommandType? commandType = null) =>
        dbConnection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);

    public Task<int> ExecuteAsync(IDbConnection dbConnection, string sql, object param = null!, IDbTransaction transaction = null!, int? commandTimeout = null,
	                              CommandType? commandType = null) =>
        dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
}