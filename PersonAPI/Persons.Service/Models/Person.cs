using System;
using Persons.Service.Extensions;

namespace Persons.Service.Models
{
    /// <summary>
    /// Информация о личности.
    /// </summary>
    public class Person
    {
        private const string defaultBirthdayFormat = "yyyy-MM-dd";
        
        /// <summary>
        /// Идентификатор(Guid).
        /// </summary>
        public Guid Id { get; }
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDay { get; }

        /// <summary>
        /// Возраст (кол-во полных лет).
        /// </summary>
        public int Age => GetAge(BirthDay);
        
        private Person(Guid id, string name, DateTime birthDay)
        {
            Id = id;
            Name = name;
            BirthDay = birthDay;
        }

        public static Person Create(Guid id, string name, DateTime birthDay)
        {
            var person = new Person(id,name,birthDay);

            return person.Name.IsNullOrEmpty() || person.Age >120 ? null : person;
        }

        public override string ToString()
        {
            return $"id:{Id}, name:{Name}, BirthDay:{BirthDay}, Age:{Age}";
        }

        public string GetFormattedBirthDay(string format = defaultBirthdayFormat)
        {
            return BirthDay.ToString(format);
        }
        
        /// <summary>
        /// Возвращает возраст с указанной даты.
        /// </summary>
        private static int GetAge(DateTime date)
        {
            var today = DateTime.Today;
            var age = today.Year - date.Year;

            if (date.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
}