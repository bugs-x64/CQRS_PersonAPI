using System;
using Persons.Service.Exceptions;
using Persons.Service.Models;

namespace Persons.Service.Repositories
{
    /// <summary>
    /// Интерфейс репозитория личностей.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Найти личность по Id.
        /// </summary>
        /// <exception cref="EntityNotFoundException{TEntity}">Личность не найдена.</exception>
        Person Find(Guid id);

        /// <summary>
        /// Добавить новую личность.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        void Insert(Person item);
    }
}