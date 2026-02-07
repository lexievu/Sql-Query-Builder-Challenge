# SQL Query Builder Challenge

### Project Structure
* `SqlQueryBuilder`: Core class library.
* `SqlQueryBuilder.Demo`: Console application demonstrating a complex query scenario.
* `SqlQueryBuilder.Tests`: xUnit test suite covering all clause combinations.

## How to Run
1. Open the solution in **Visual Studio 2026**.
2. Set `SqlQueryBuilder.Demo` as the Startup Project.
3. Run the application (F5).
4. It will print the generated SQL for the complex query scenario required in the spec.

## Implementation Notes
* **Type Safety:** I used C# Records (`SqlColumn`) and Enums (`JointType`, `Operator`) to avoid magic strings.
* **Recursive Where:** The `IWhere` interface allows nesting `AND`/`OR` clauses infinitely (e.g. `(A OR B) AND C`).
* **Formatting:** DateTimes are formatted to SQL-safe strings (`yyyy-MM-dd...`), and strings are wrapped in single quotes.

## Limitations
Due to the time constraints (1-2 hours), the following features were explicitly treated as **Out of Scope**:

** **SQL Injection:** Currently, values are sanitised directly into the string.
* **Operators:** The current implementation supports standard comparison operators (`=`, `!=`, `<`, `>`, etc.). Advanced operators like `IN`, `BETWEEN`, `LIKE`, and `IS NULL` are not yet supported.
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