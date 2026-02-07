namespace SqlQueryBuilder;

public enum JointType
{
    Inner,
    LeftOuter,
    RightOuter,
    FullOuter
}

public static class JointTypeConversion
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
            case JointType.FullOuter:
                return "FULL OUTER";
            default:
                throw new ArgumentException(nameof(JointType));
        }
    }
}