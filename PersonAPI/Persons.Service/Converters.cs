using System;
using System.Globalization;

namespace Persons.Service
{
    /// <summary>
    /// Конвертер типов.
    /// </summary>
    public static class Converter
    {
        private static IFormatProvider DateTimeFormatInfo => new DateTimeFormatInfo();

        /// <summary>
        /// Преобразует строку в дату.
        /// </summary>
        public static DateTime ToDateTime(string date)
        {
            try
            {
                return DateTime.Parse(date, DateTimeFormatInfo);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Не удалось преобразовать в дату {date}.", e);
            }
        }
    }
}