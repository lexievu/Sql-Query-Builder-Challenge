using System.Text;

namespace SqlQueryBuilder;

public class QueryBuilder
{
    private readonly StringBuilder _stringBuilder = new();
    private string _tableName = string.Empty;
    private SqlColumn[] _selectedColumns = [];
    private readonly List<SqlJoin> _joins = [];
    private IWhere? _where = null;
    
    public QueryBuilder From(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public QueryBuilder Select(SqlColumn[] columns)
    {
        _selectedColumns = columns;
        return this;
    }

    public QueryBuilder Join(string table, SqlColumn left, SqlColumn right, JointType jointType)
    {
        _joins.Add(new SqlJoin(table, left, right, jointType));
        return this;
    }

    public QueryBuilder Where(IWhere where)
    {
        _where = where;
        return this;
    }

    public void Clear()
    {
        _stringBuilder.Clear();
        _tableName = string.Empty;
        _selectedColumns = [];
        _joins.Clear();
        _where = null;
    }
    
    public string Build()
    {
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
        
        return _stringBuilder.ToString();
    }
}

