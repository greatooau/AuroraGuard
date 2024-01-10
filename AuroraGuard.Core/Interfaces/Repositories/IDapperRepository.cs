using System.Data;

namespace AuroraGuard.Core.Interfaces.Repositories;

public interface IDapperRepository
{
	Task<IEnumerable<T>> QueryAsync<T>(IDbConnection dbConnection, string sql, object param = null!, 
	                                   IDbTransaction transaction = null!,
	                                   int? commandTimeout = null,
	                                   CommandType? commandType = null);

	Task<T?> QuerySingleOrDefaultAsync<T>(IDbConnection dbConnection, string sql, object param = null!,
	                                      IDbTransaction transaction = null!,
	                                      int? commandTimeout = null,
	                                      CommandType? commandType = null);

	Task<T> QuerySingleAsync<T>(IDbConnection dbConnection, string sql,
	                            object param = null!,
	                            IDbTransaction transaction = null!,
	                            int? commandTimeout = null,
	                            CommandType? commandType = null);

	Task<int> ExecuteAsync(IDbConnection dbConnection, string sql,
	                       object param = null!,
	                       IDbTransaction transaction = null!,
	                       int? commandTimeout = null,
	                       CommandType? commandType = null);
}