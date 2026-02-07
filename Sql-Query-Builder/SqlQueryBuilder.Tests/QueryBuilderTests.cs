using SqlQueryBuilder;

namespace SqlQueryBuilderTests;

public class QueryBuilderTests
{
    [Fact]
    public void Builder()
    {
        Assert.Equal(string.Empty, new QueryBuilder().Build());
    }

    [Fact]
    public void SimpleSelect()
    {
        var queryBuilder = new QueryBuilder()
            .From("Event")
            .Select([new SqlColumn("Event", "Id"), new SqlColumn("Event", "Name")])
            .Build();
        
        var expected = "SELECT Event.Id, Event.Name FROM Event";
        
        Assert.Equal(expected, queryBuilder);
    }

    [Fact]
    public void SelectWithAlias()
    {
        var queryBuilder = new QueryBuilder()
            .From("Event")
            .Select([new SqlColumn("Event", "Id", "I"), new SqlColumn("Event", "Name", "N")])
            .Build();
        
        var expected = "SELECT Event.Id AS \"I\", Event.Name AS \"N\" FROM Event";
        
        Assert.Equal(expected, queryBuilder);
    }
    
    [Fact]
    public void SelectWithAliasSpace()
    {
        var queryBuilder = new QueryBuilder()
            .From("Event")
            .Select([new SqlColumn("Event", "Id", "Event Id"), new SqlColumn("Event", "Name", "N")])
            .Build();
        
        var expected = "SELECT Event.Id AS \"Event Id\", Event.Name AS \"N\" FROM Event";
        
        Assert.Equal(expected, queryBuilder);
    }

    [Fact]
    public void JoinWithOneTable()
    {
        var queryBuilder = new QueryBuilder()
            .From("Event")
            .Select([new SqlColumn("Event", "Id", "I"), new SqlColumn("Event", "Name", "N")])
            .Join("EventAttendee", new SqlColumn("Event", "Id"), new SqlColumn("EventAttendee", "EventId"), JointType.Inner)
            .Build();
        
        var expected = "SELECT Event.Id AS \"I\", Event.Name AS \"N\" FROM Event INNER JOIN EventAttendee ON Event.Id = EventAttendee.EventId";
        
        Assert.Equal(expected, queryBuilder);
    }
    
    [Fact]
    public void JoinWithTwoTables()
    {
        var queryBuilder = new QueryBuilder()
            .From("Event")
            .Select([new SqlColumn("Event", "Id", "I"), new SqlColumn("Event", "Name", "N")])
            .Join("EventAttendee", new SqlColumn("Event", "Id"), new SqlColumn("EventAttendee", "EventId"), JointType.Inner)
            .Join("Attendee", new SqlColumn("EventAttendee", "AttendeeId"), new SqlColumn("Attendee", "Id"), JointType.LeftOuter)
            .Build();
        
        var expected = "SELECT Event.Id AS \"I\", Event.Name AS \"N\" " +
                       "FROM Event " +
                       "INNER JOIN EventAttendee ON Event.Id = EventAttendee.EventId " +
                       "LEFT OUTER JOIN Attendee ON EventAttendee.AttendeeId = Attendee.Id";
        
        Assert.Equal(expected, queryBuilder);
    }

    [Fact]
    public void QueryBuilderClear()
    {
        var queryBuilder = new QueryBuilder()
            .From("Event")
            .Select([new SqlColumn("Event", "Id", "I"), new SqlColumn("Event", "Name", "N")])
            .Join("EventAttendee", new SqlColumn("Event", "Id"), new SqlColumn("EventAttendee", "EventId"), JointType.Inner)
            .Join("Attendee", new SqlColumn("EventAttendee", "AttendeeId"), new SqlColumn("Attendee", "Id"), JointType.LeftOuter);
        queryBuilder.Clear();
        
        Assert.Equal(string.Empty, queryBuilder.Build());
    }
    
    [Fact]
    public void SimpleWhere()
    {
        var queryBuilder = new QueryBuilder()
            .From("Event")
            .Select([new SqlColumn("Event", "Id"), new SqlColumn("Event", "Name")])
            .Where(new SqlWhere(new SqlColumn("Event", "Id"), Operator.Equals, 2))
            .Build();
        
        var expected = "SELECT Event.Id, Event.Name FROM Event WHERE Event.Id = 2";
        
        Assert.Equal(expected, queryBuilder);
    }
    
    [Fact]
    public void Ultimate()
    {
        var query = new QueryBuilder()
            .From("Event")
            .Join("EventAttendee", new SqlColumn("Event", "Id"), new SqlColumn("EventAttendee", "EventId"), JointType.Inner)
            .Join("Attendee", new SqlColumn("EventAttendee", "AttendeeId"), new SqlColumn("Attendee", "Id"), JointType.LeftOuter)
            .Select([new SqlColumn("Event", "Name", "I"), new SqlColumn("Attendee", "Name", "N")])
            .Where(new Or([new SqlWhere(new SqlColumn("Attendee", "Name"), Operator.Equals, "Bob"),
                new SqlWhere(new SqlColumn("Event", "Important"), Operator.GreaterThanOrEquals, 1)]))
            .Build();
        
        var expected = "SELECT Event.Name AS \"I\", Attendee.Name AS \"N\" " +
                       "FROM Event " +
                       "INNER JOIN EventAttendee ON Event.Id = EventAttendee.EventId " +
                       "LEFT OUTER JOIN Attendee ON EventAttendee.AttendeeId = Attendee.Id " +
                       "WHERE (Attendee.Name = 'Bob' OR Event.Important >= 1)";
        
        Assert.Equal(expected, query);
    }
}