using System.Text;

namespace SqlQueryBuilder;

/// <inheritdoc/>
public class QueryBuilder : IQueryBuilder
{
    private readonly StringBuilder _stringBuilder = new();
    private string _tableName = string.Empty;
    private SqlColumn[] _selectedColumns = [];
    private readonly List<SqlJoin> _joins = [];
    private IWhere? _where = null;
    
    // LIGHTWEIGHT LOGGING: Inject a simple action instead of a heavy ILogger
    private readonly Action<string>? _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
    /// </summary>
    /// <param name="logger">Optional action to log the generated SQL (e.g. Console.WriteLine).</param>
    public QueryBuilder(Action<string>? logger = null)
    {
        _logger = logger;
    }
    
    public IQueryBuilder From(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public IQueryBuilder Select(SqlColumn[] columns)
    {
        _selectedColumns = columns;
        return this;
    }

    public IQueryBuilder Join(string table, SqlColumn left, SqlColumn right, JointType jointType)
    {
        _joins.Add(new SqlJoin(table, left, right, jointType));
        return this;
    }

    public IQueryBuilder Where(IWhere where)
    {
        _where = where;
        return this;
    }

    public string Build()
    {
        if (string.IsNullOrEmpty(_tableName))
        {
            throw new InvalidOperationException("Query must have a FROM clause defined.");
        }

        if (_selectedColumns.Length == 0)
        {
            throw new InvalidOperationException("Query must select at least one column.");
        }

        _stringBuilder.Clear();

        if (_selectedColumns.Length > 0)
        {
            _stringBuilder.Append("SELECT ");
            _stringBuilder.Append(string.Join(", ", _selectedColumns.Select(c => c.ToString())));
        }

        if (!string.IsNullOrEmpty(_tableName))
        {
            _stringBuilder.Append(" FROM ");
            _stringBuilder.Append(_tableName);
        }

        if (_joins.Count > 0)
        {
            _stringBuilder.Append(' ');
            _stringBuilder.Append(string.Join(" ", _joins.Select(c => c.ToString())));
        }

        if (_where != null)
        {
            _stringBuilder.Append(" WHERE ");
            _stringBuilder.Append(_where.ToString());
        }
        
        var sql = _stringBuilder.ToString();
        _logger?.Invoke($"[{nameof(QueryBuilder)}.{nameof(Build)}] Generated SQL: {sql}");
        return sql;
    }
}

