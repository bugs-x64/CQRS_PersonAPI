using Persons.Service.Constants;

namespace Persons.Service.Extensions
{
    /// <summary>
    /// Расширения для <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Проверят строку на null и пустоту.
        /// </summary>
        /// <returns>true - строка пустая или null. false - строка не пустая.</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Форматирует строку настройками из <see cref="GlobalConstants"/>.
        /// </summary>
        /// <returns>Форматированная строка.</returns>
        public static string DefaultFormat(this string format)
        {
            return string.Format(GlobalConstants.DefaultCultureInfo, format);
        }

        /// <summary>
        /// Форматирует строку настройками из <see cref="GlobalConstants"/>.
        /// </summary>
        /// <returns>Форматированная строка.</returns>
        public static string DefaultFormat(this string format, object arg0)
        {
            return string.Format(GlobalConstants.DefaultCultureInfo, format, arg0);
        }

        /// <summary>
        /// Форматирует строку настройками из <see cref="GlobalConstants"/>.
        /// </summary>
        /// <returns>Форматированная строка.</returns>
        public static string DefaultFormat(this string format, object arg0, object arg1)
        {
            return string.Format(GlobalConstants.DefaultCultureInfo, format, arg0, arg1);
        }

        /// <summary>
        /// Форматирует строку настройками из <see cref="GlobalConstants"/>.
        /// </summary>
        /// <returns>Форматированная строка.</returns>
        public static string DefaultFormat(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(GlobalConstants.DefaultCultureInfo, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Форматирует строку настройками из <see cref="GlobalConstants"/>.
        /// </summary>
        /// <returns>Форматированная строка.</returns>
        public static string DefaultFormat(this string format, params object[] args)
        {
            return string.Format(GlobalConstants.DefaultCultureInfo, format, args);
        }
    }
}