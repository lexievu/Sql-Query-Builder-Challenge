using System.Text;

namespace SqlQueryBuilderCSharp;

public class QueryBuilder
{
    private readonly StringBuilder _stringBuilder = new();
    private string _tableName = string.Empty;
    private SqlColumn[] _selectedColumns = [];
    
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
        
        return _stringBuilder.ToString();
    }
}

public record SqlColumn(string TableName, string ColumnName, string? ColumnAlias = null)
{
    public override string ToString()
    {
        if (string.IsNullOrEmpty(ColumnAlias))
            return $"{TableName}.{ColumnName}";
        else
            return $"{TableName}.{ColumnName} AS {ColumnAlias}";
    }
}