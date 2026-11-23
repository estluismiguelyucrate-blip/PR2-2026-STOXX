namespace Stoxx.Web;

/// <summary>
/// Centraliza las fechas de negocio en la zona horaria de Bolivia,
/// independientemente de la configuración del servidor.
/// </summary>
public static class BusinessTime
{
    private static readonly TimeZoneInfo BoliviaTimeZone = TimeZoneInfo.CreateCustomTimeZone(
        id: "America/La_Paz",
        baseUtcOffset: TimeSpan.FromHours(-4),
        displayName: "Hora de Bolivia",
        standardDisplayName: "Hora de Bolivia");

    public static DateTime Now => TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, BoliviaTimeZone).DateTime;

    public static DateOnly Today => DateOnly.FromDateTime(Now);

    public static (DateTime StartUtc, DateTime EndUtc) UtcRangeForDay(DateOnly day)
    {
        var localStart = DateTime.SpecifyKind(day.ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);
        var localEnd = DateTime.SpecifyKind(day.AddDays(1).ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);

        return (
            TimeZoneInfo.ConvertTimeToUtc(localStart, BoliviaTimeZone),
            TimeZoneInfo.ConvertTimeToUtc(localEnd, BoliviaTimeZone));
    }

    public static DateTime ToLocal(DateTime utcDateTime)
    {
        var utc = utcDateTime.Kind == DateTimeKind.Utc
            ? utcDateTime
            : DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

        return TimeZoneInfo.ConvertTimeFromUtc(utc, BoliviaTimeZone);
    }
}
