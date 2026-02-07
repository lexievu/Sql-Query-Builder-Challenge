# SQL Query Builder Challenge

## Project Structure
* `SqlQueryBuilderCSharp`: Main library + Console "Demo" application.
* `SqlQueryBuilderTests`: xUnit test suite.

## How to Run
1. Open the solution in VS 2026.
2. Run `SqlQueryBuilderCSharp` (Console App).
3. It will print the SQL output for a complex query scenario required in the spec.

## Implementation Notes
* **Type Safety:** I used C# Records (`SqlColumn`) and Enums (`JointType`, `Operator`) to avoid magic strings.
* **Recursive Where:** The `IWhere` interface allows nesting `AND`/`OR` clauses infinitely (e.g. `(A OR B) AND C`).
* **Formatting:** DateTimes are formatted to SQL-safe strings (`yyyy-MM-dd...`), and strings are wrapped in single quotes.

## Limitations / Future Improvements
* **SQL Injection:** Currently, values are sanitised directly into the string.
* **Aggregates:** Functions like `COUNT`, `MAX`, or `SUM` are not currently supported in the `SqlColumn` object structure.
* **Complex Selections:** Subqueries and CTEs are out of scope for this lightweight implementation but could be added by allowing `From()` to accept a `QueryBuilder` instance.
* **Joint Type:** `CROSS JOIN` is intentionally omitted as it requires a different data structure (no `ON` clause).
* **Architecture:** Currently uses a Builder pattern where the `QueryBuilder` orchestrates the SQL order. For a more extensible design (e.g. supporting CTEs or Union clauses), this could be refactored into an Interpreter pattern where each clause is an independent `IQueryPart`.

## Example Usage
```csharp
var query = new QueryBuilder()
            .From("Event")
            .Join("EventAttendee", new SqlColumn("Event", "Id"), new SqlColumn("EventAttendee", "EventId"), JointType.Inner)
            .Join("Attendee", new SqlColumn("EventAttendee", "AttendeeId"), new SqlColumn("Attendee", "Id"), JointType.LeftOuter)
            .Select([new SqlColumn("Event", "Name", "I"), new SqlColumn("Attendee", "Name", "N")])
            .Where(new Or([new SqlWhere(new SqlColumn("Attendee", "Name"), Operator.Equals, "Bob"),
                new SqlWhere(new SqlColumn("Event", "Important"), Operator.GreaterThanOrEquals, 1)]))
            .Build();
    
// SELECT Event.Name AS I, Attendee.Name AS N
// FROM Event
// INNER JOIN EventAttendee ON Event.Id = EventAttendee.EventId 
// LEFT OUTER JOIN Attendee ON EventAttendee.AttendeeId = Attendee.Id 
// WHERE (Attendee.Name = 'Bob' OR Event.Important >= 1)