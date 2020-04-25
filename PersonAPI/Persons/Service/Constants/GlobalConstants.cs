using System.Globalization;

namespace Persons
{
    public static class GlobalConstants
    {
        public const string Locale = "ru-RU";
        public static readonly CultureInfo DefaultCultureInfo = new CultureInfo(Locale);
    }
}