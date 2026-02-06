using System.Text;

namespace SqlQueryBuilderCSharp;

public class QueryBuilder
{
    private readonly StringBuilder _stringBuilder = new();
    private string _tableName = string.Empty;
    private SqlColumn[] _selectedColumns = [];
    private List<SqlJoins> _joins = [];
    
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
        _joins.Add(new SqlJoins(table, left, right, jointType));
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

        if (_joins.Count > 0)
        {
            _stringBuilder.Append(" ");
            _stringBuilder.Append(string.Join(" ", _joins.Select(c => c.ToString())));
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

public record SqlJoins(string table, SqlColumn left, SqlColumn right, JointType JointType)
{
    public override string ToString()
    {
        return $"{JointType.ToSqlString()} JOIN {table} ON {left.TableName}.{left.ColumnName} = {right.TableName}.{right.ColumnName}";
    }
}

public enum JointType
{
    Inner,
    LeftOuter,
    RightOuter
}

public static class JoinTypeConversion
{
    public static string ToSqlString(this JointType jointType)
    {
        switch (jointType)
        {
            case JointType.Inner:
                return "INNER";
            case JointType.LeftOuter:
                return "LEFT OUTER";
            case JointType.RightOuter:
                return "RIGHT OUTER";
        }
    }
}