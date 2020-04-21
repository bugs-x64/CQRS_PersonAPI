using System;

namespace Persons.Service
{
    /// <summary>
    /// Информация о личности.
    /// </summary>
    public class Person
    {
        private Guid _id;
        private DateTime _birthDate;

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
        public string BirthDate
        {
            get => $"{_birthDate:yyyy-MM-d}";
            set
            {
                try
                {
                    _birthDate = DateTime.Parse(value);

                    
                    var age = Convert.ToInt32(DateTime.UtcNow.ToString("yyyy"))- Convert.ToInt32(_birthDate.ToUniversalTime().ToString("yyyy"));
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
            return $"id:{_id}, name:{Name}, BirthDate:{BirthDate}, Age:{Age}";
        }
    }
}