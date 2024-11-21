using System.Globalization;

namespace RisksSystem.Utils.Extensions
{
    public static class GeneralExt
    {
        public static double ConvertToDouble(this string value, out bool valid)
        {
            double v;
            valid = double.TryParse(value, out v);

            return v;
        }

        public static int ConvertToInt(this string value, out bool valid)
        {
            int v;
            valid = int.TryParse(value, out v);

            return v;
        }

        public static DateTime ConvertToDate(this string value, out bool valid)
        {
            string format = "MM/dd/yyyy";
            CultureInfo culture = CultureInfo.InvariantCulture;
            DateTime date;
            valid = DateTime.TryParseExact(value, format, culture, DateTimeStyles.None, out date);

            return date;
        }
    }
}
