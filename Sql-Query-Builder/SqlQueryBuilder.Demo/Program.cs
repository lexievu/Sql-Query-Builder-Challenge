using SqlQueryBuilder;

var query = new QueryBuilder(logMessage => Console.WriteLine($"DEBUG: {logMessage}"))
    .From("Event")
    .Join("EventAttendee", new SqlColumn("Event", "Id"), new SqlColumn("EventAttendee", "EventId"), JointType.Inner)
    .Join("Attendee", new SqlColumn("EventAttendee", "AttendeeId"), new SqlColumn("Attendee", "Id"), JointType.LeftOuter)
    .Select([new SqlColumn("Event", "Name", "I"), new SqlColumn("Attendee", "Name", "N")])
    .Where(new Or([new SqlWhere(new SqlColumn("Attendee", "Name"), Operator.Equals, "Bob"),
        new SqlWhere(new SqlColumn("Event", "Important"), Operator.GreaterThanOrEquals, 1)]))
    .Build();

Console.WriteLine("---Generated SQL---");
Console.WriteLine(query);