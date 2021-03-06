﻿namespace Persons.Service.Dto
{
    /// <summary>
    /// Dto информации о Person.
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Идентификатор(Guid).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// Возраст (кол-во полных лет).
        /// </summary>
        public int? Age { get; set; }
    }
}