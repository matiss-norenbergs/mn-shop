namespace MN_Shop.Server.Methods
{
    public static class DateTimeMethods
    {
        public static DateTime UtcToLocal(this DateTime dateTime, string defaultTimeZone)
        {
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(defaultTimeZone);
                var dateAndOffset = new DateTimeOffset(dateTime, tz.GetUtcOffset(dateTime));

                return dateAndOffset.DateTime.Add(dateAndOffset.Offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return dateTime;
            }
        }
    }
}
