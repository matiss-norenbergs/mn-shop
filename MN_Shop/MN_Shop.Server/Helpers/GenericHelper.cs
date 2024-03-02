namespace MN_Shop.Server.Helpers
{
    public static class GenericHelper
    {
        public static Dictionary<string, TimeZoneInfo> GetTimeZoneCollection()
        {
            var timeZones = new Dictionary<string, TimeZoneInfo>();
            try
            {
                foreach (var item in TimeZoneInfo.GetSystemTimeZones())
                    timeZones.Add(item.Id, item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return timeZones;
        }
    }
}
