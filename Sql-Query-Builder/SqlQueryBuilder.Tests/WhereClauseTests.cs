using SqlQueryBuilder;

namespace SqlQueryBuilderTests;

public class WhereClausesTests
{
    [Fact]
    public void SimpleWhereClause()
    {
        var where = new SqlWhere(new SqlColumn("Event", "Id"), Operator.Equals, 2);
        var expected = "Event.Id = 2";
        var actual = where.ToString();
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void DateTimeWhereClause()
    {
        var where = new SqlWhere(new SqlColumn("Event", "Date"), Operator.GreaterThan, new DateTime(2026, 1, 1));
        var expected = "Event.Date > '2026-01-01 00:00:00.000'";
        var actual = where.ToString();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AndWhereClause()
    {
        var where1 = new SqlWhere(new SqlColumn("Event", "Id"), Operator.Equals, 2);
        var where2 = new SqlWhere(new SqlColumn("Event", "Name"), Operator.NotEquals, "TestName");
        var and = new And([where1, where2]);
        
        var expected = "(Event.Id = 2 AND Event.Name != 'TestName')";
        var actual = and.ToString();
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void OrWhereClause()
    {
        var where1 = new SqlWhere(new SqlColumn("Event", "Id"), Operator.Equals, 2);
        var where2 = new SqlWhere(new SqlColumn("Event", "Name"), Operator.NotEquals, "TestName");
        var or = new Or([where1, where2]);
        
        var expected = "(Event.Id = 2 OR Event.Name != 'TestName')";
        var actual = or.ToString();
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void CombiWhereClause()
    {
        var where1 = new SqlWhere(new SqlColumn("Event", "Id"), Operator.Equals, 2);
        var where2 = new SqlWhere(new SqlColumn("Event", "Name"), Operator.NotEquals, "TestName");
        var or = new Or([where1, where2]);
        var where3 = new SqlWhere(new SqlColumn("Event", "CreatedBy"), Operator.NotEquals, "PersonA");
        var where4 = new SqlWhere(new SqlColumn("Event", "A"), Operator.GreaterThan, 23);
        var where5 = new SqlWhere(new SqlColumn("Event", "B"), Operator.LessThan, 0);
        var and = new And([or, where3, new And([where4, where5])]);
        
        var expected = "((Event.Id = 2 OR Event.Name != 'TestName') " +
                       "AND Event.CreatedBy != 'PersonA' " +
                       "AND (Event.A > 23 AND Event.B < 0))";
        var actual = and.ToString();
        Assert.Equal(expected, actual);
    }
}