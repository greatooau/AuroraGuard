using System.Data;

namespace AuroraGuard.Core.Interfaces.Repositories;

public interface IDapperRepository
{
	Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null!, 
	                                   IDbTransaction transaction = null!,
	                                   int? commandTimeout = null,
	                                   CommandType? commandType = null);

	Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object param = null!,
	                                      IDbTransaction transaction = null!,
	                                      int? commandTimeout = null,
	                                      CommandType? commandType = null);

	Task<T> QuerySingleAsync<T>(string sql,
	                            object param = null!,
	                            IDbTransaction transaction = null!,
	                            int? commandTimeout = null,
	                            CommandType? commandType = null);

	Task<int> ExecuteAsync(string sql,
	                       object param = null!,
	                       IDbTransaction transaction = null!,
	                       int? commandTimeout = null,
	                       CommandType? commandType = null);
}