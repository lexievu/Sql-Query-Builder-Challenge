namespace SqlQueryBuilder;

public interface IQueryBuilder
{
    IQueryBuilder From(string tableName);
    IQueryBuilder Select(SqlColumn[] columns);
    IQueryBuilder Join(string table, SqlColumn left, SqlColumn right, JointType jointType);
    IQueryBuilder Where(IWhere where);
    void Clear();
    string Build();
}