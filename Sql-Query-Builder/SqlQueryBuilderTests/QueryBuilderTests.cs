using SqlQueryBuilderCSharp;

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
        
        var expected = "SELECT Event.Id AS I, Event.Name AS N FROM Event";
        
        Assert.Equal(expected, queryBuilder);
    }
    
    [Fact]
    public void Ultimate()
    {
        // var query = new QueryBuilder()
        //     .From("Events")
        //     .Join(JoinType.Inner, "Events.Id", "EventAttendee.EventId")
        //     .Join(JoinType.Inner, "EventAttendee.AttendeeId", "Attendee.Id")
        //     .Where("Attendee.Name = \"Bob\" or Events.Important = 1")
        //     .Select("Events.Name", "Attendee.Name")
        //     .Build();
        //
        // var expected = "SELECT Events.Name, Attendee.Name" +
        //                "FROM Events" +
        //                "INNER JOIN EventAttendee ON Events.Id = EventAttendee.EventId" +
        //                "INNER JOIN Attendee ON Attendee.Id = EventAttendee.AttendeeId" +
        //                "WHERE Attendee.Name = \"Bob\" OR Events.Important = 1;";
        //
        // Assert.Equal(expected, query);
    }
}