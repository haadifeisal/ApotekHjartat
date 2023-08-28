namespace ApotekHjartat.Order.Api.Helpers
{
    public class Helper
    {
        public static string ConvertDateTimeToString(DateTime date)
        {
            return date.ToString("yyyyMMddHHmmss");
        }
    }
}
