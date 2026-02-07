namespace SqlQueryBuilder;

/// <summary>
/// A fluent builder for generating T-SQL queries programmatically.
/// </summary>
public interface IQueryBuilder
{
    /// <summary>
    /// Specifies the table to select data from.
    /// </summary>
    /// <param name="tableName">The name of the database table.</param>
    /// <returns>The builder instance for chaining.</returns>
    IQueryBuilder From(string tableName);
    
    /// <summary>
    /// Specifies the columns to include in the result set.
    /// </summary>
    /// <param name="columns">An array of <see cref="SqlColumn"/> objects representing the columns.</param>
    /// <returns>The builder instance.</returns>
    IQueryBuilder Select(SqlColumn[] columns);
    
    /// <summary>
    /// Adds a JOIN clause to the query.
    /// </summary>
    /// <param name="table">The name of the table to join.</param>
    /// <param name="left">The column from the left table.</param>
    /// <param name="right">The column from the right (joining) table.</param>
    /// <param name="jointType">The type of join (Inner, Left, Right, Full).</param>
    /// <returns>The builder instance.</returns>
    IQueryBuilder Join(string table, SqlColumn left, SqlColumn right, JointType jointType);
    
    /// <summary>
    /// Adds a WHERE clause to filter the results.
    /// </summary>
    /// <param name="where">A <see cref="SqlWhere"/> condition or a combined <see cref="And"/>/<see cref="Or"/> clause.</param>
    /// <returns>The builder instance.</returns>
    IQueryBuilder Where(IWhere where);
    
    /// <summary>
    /// Compiles the query components into a standard T-SQL string.
    /// </summary>
    /// <returns>A string containing the valid SQL query.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no Table or Columns are specified.</exception>
    string Build();
}