using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persons.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория личностей.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Найти личность по Id.
        /// </summary>
        Person Find(Guid id);

        /// <summary>
        /// Добавить новую личность.
        /// </summary>
        void Insert(Person item);
    }
}