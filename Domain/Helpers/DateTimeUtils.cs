namespace Domain.Helpers;

public static class DateTimeExtenstions
{
    /// <summary>
    /// DateTimeから月末のDateTimeを返却します。
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime EndOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

    /// <summary>
    /// 月初のDateTimeを返却します。
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime StartOfMonth(this DateTime date) => new(date.Year, date.Month, 1);
}