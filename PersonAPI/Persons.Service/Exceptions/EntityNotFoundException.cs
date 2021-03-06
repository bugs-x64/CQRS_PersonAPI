﻿using System;
using Persons.Service.Extensions;

namespace Persons.Service.Exceptions
{
    /// <summary>
    /// Ошибка не найденной сущности.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    [Serializable]
    public class EntityNotFoundException<TEntity> : Exception
    {
        public EntityNotFoundException() : base(Resources.EntityNotFoundException.DefaultFormat(typeof(TEntity)))
        {
        }
    }
}