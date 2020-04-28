using System.Globalization;

namespace Persons.Service.Constants
{
    /// <summary>
    /// Глобальные константы.
    /// </summary>
    public static class GlobalConstants
    {
        /// <summary>
        /// Текущая локаль.
        /// </summary>
        public const string Locale = "ru-RU";//вобще ее можно в конфиг или параметры приложения, но в данном случае это не требуется.
        public static readonly CultureInfo DefaultCultureInfo = new CultureInfo(Locale);
    }
}