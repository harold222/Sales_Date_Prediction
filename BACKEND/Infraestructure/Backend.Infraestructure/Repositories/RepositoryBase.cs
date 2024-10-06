using Backend.Application.Contracts.Persistence;
using Backend.Application.Exceptions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Dynamic;

namespace Backend.Infraestructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : class
{
    private readonly IConfiguration _configuration;

    public RepositoryBase(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default!)
    {
        using (SqlConnection connection = new(_configuration.GetConnectionString("Database")))
        {
            await connection.OpenAsync();

            string tableName = GetTableName();
            string columns = GetColumns();
            string values = GetPropertyNames();

            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

            await connection.ExecuteAsync(query, entity);

            return entity;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(List<string> specificColumns, CancellationToken cancellationToken = default!)
    {
        using SqlConnection _connection = new(_configuration.GetConnectionString("Database"));

        string tableName = GetTableName();
        string columns = "";

        if (specificColumns is not null && specificColumns.Any())
            columns = string.Join(", ", specificColumns);
        else
            columns = GetColumns();


        string query = $"SELECT {columns} FROM {tableName}";

        await _connection.OpenAsync();

        return await _connection.QueryAsync<T>(
            new CommandDefinition(query, cancellationToken)
        );
    }

    public async Task<T> GetByIdAsync(int id, List<string> specificColumns)
    {
        using (SqlConnection connection = new(_configuration.GetConnectionString("Database")))
        {
            await connection.OpenAsync();

            string tableName = GetTableName();
            string keyColumn = GetKeyColumnName();

            string columns = "";

            if (specificColumns is not null && specificColumns.Any())
                columns = string.Join(", ", specificColumns);
            else
                columns = GetColumns();

            string query = $"SELECT {columns} FROM {tableName} WHERE {keyColumn} = @Id";

            T? dbSearch = await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });

            if (dbSearch is null)
                throw new NotFoundException(tableName, id);

            return dbSearch;
        }
    }

    public async Task<IEnumerable<T>> QueryStoredProcedureAsync(string storedProcedureName, object paramProc = null)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Database")))
        {
            await connection.OpenAsync();

            return await connection.QueryAsync<T>(storedProcedureName, paramProc, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<IDictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName, object paramProc = null, Dictionary<string, DbType> parameters = null)
    {
        var bd = _configuration.GetConnectionString("Database");
        using (SqlConnection connection = new SqlConnection(bd))
        {
            await connection.OpenAsync();

            DynamicParameters param = new();

            if (paramProc != null)
            {
                PropertyInfo[] properties = paramProc.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    param.Add(property.Name, property.GetValue(paramProc));
                }
            }

            if (parameters != null)
                foreach (KeyValuePair<string, DbType> parameter in parameters)
                {
                    param.Add(parameter.Key, null, parameter.Value, ParameterDirection.Output);
                }

            await connection.ExecuteAsync(storedProcedureName, param, commandType: CommandType.StoredProcedure);

            IDictionary<string, object> objectResp = new ExpandoObject() as IDictionary<string, Object>;

            if (parameters != null)
                foreach (KeyValuePair<string, DbType> parameter in parameters)
                {
                    objectResp.Add(parameter.Key, param.Get<object>(parameter.Key));
                }

            return objectResp;
        }
    }

    private string GetKeyColumnName()
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

            if (keyAttributes != null && keyAttributes.Length > 0)
                return property.Name;
        }

        return string.Empty;
    }

    private string GetTableName()
    {
        string tableName = "";
        var type = typeof(T);
        var tableAttr = type.GetCustomAttribute<TableAttribute>();

        if (tableAttr != null)
        {
            tableName = tableAttr.Name;
            return tableName;
        }

        return type.Name;
    }

    private string GetColumns(bool excludeKey = false)
    {
        var type = typeof(T);

        var columns = string.Join(", ", type.GetProperties()
            .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
            .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null)
            .Select(p =>
            {
                ColumnAttribute? columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                return columnAttr != null ? columnAttr.Name : p.Name;
            }));

        return columns;
    }

    private string GetPropertyNames(bool excludeKey = false)
    {
        var type = typeof(T);

        var properties = type.GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null)
            .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null);

        string values = string.Join(", ", properties.Select(p =>
        {
            return $"@{p.Name}";
        }));

        return values;
    }

    protected string GetKeyPropertyName()
    {
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);

        if (properties.Any())
            return properties.FirstOrDefault()!.Name;

        return string.Empty;
    }
}
