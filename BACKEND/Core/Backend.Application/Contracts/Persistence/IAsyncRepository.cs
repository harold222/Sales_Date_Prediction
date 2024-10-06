using System.Data;

namespace Backend.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(List<string> specificColumns = null, CancellationToken cancellationToken = default!);

    Task<T> GetByIdAsync(int id, List<string> specificColumns = null);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default!);

    Task<IEnumerable<T>> QueryStoredProcedureAsync(string storedProcedureName, object paramProc = null);
    Task<IDictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName, object paramProc = null, Dictionary<string, DbType> parameters = null);
}
