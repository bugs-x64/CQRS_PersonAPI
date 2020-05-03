using System;
using System.Globalization;

namespace Persons.Service
{
    /// <summary>
    /// Конвертер типов.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Преобразует строку в дату.
        /// </summary>
        public static DateTime ToDateTime(string date)
        {
            try
            {
                var dateTimeFormatInfo = new DateTimeFormatInfo();

                return DateTime.Parse(date, dateTimeFormatInfo);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Не удалось преобразовать в дату {date}.", e);
            }
        }
    }
}