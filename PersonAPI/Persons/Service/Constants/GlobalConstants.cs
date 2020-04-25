using System.Globalization;

namespace Persons.Service.Constants
{
    public static class GlobalConstants
    {
        public const string Locale = "ru-RU";
        public static readonly CultureInfo DefaultCultureInfo = new CultureInfo(Locale);
    }
}