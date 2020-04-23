﻿using System;
using System.Globalization;

namespace Persons.Service
{
    /// <summary>
    /// Информация о личности.
    /// </summary>
    public class Person
    {
        private Guid _id;
        private DateTime _birthDay;

        /// <summary>
        /// Идентификатор(Guid).
        /// </summary>
        public string Id { get => _id.ToString(); set => _id = Guid.Parse(value); }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public string BirthDay
        {
            get => $"{_birthDay:yyyy-MM-d}";
            set
            {
                try
                {
                    const string yearFormat = "yyyy";
                    var dateTimeFormatInfo = new CultureInfo(GlobalConstants.Locale);

                    _birthDay = DateTime.Parse(value,dateTimeFormatInfo);
                    
                    var yearBirth = _birthDay.ToUniversalTime().ToString(yearFormat,dateTimeFormatInfo);
                    var yearNow = DateTime.UtcNow.ToString(yearFormat,dateTimeFormatInfo);
                    var age = Convert.ToInt32(yearNow,dateTimeFormatInfo)- Convert.ToInt32(yearBirth,dateTimeFormatInfo);

                    Age = age <= 120 ? age : (int?)null;
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Не удалось преобразовать в дату {value}.",e);
                }
            }
        }

        /// <summary>
        /// Возраст (кол-во полных лет).
        /// </summary>
        public int? Age { get; private set; }

        public override string ToString()
        {
            return $"id:{_id}, name:{Name}, BirthDay:{BirthDay}, Age:{Age}";
        }
    }
}