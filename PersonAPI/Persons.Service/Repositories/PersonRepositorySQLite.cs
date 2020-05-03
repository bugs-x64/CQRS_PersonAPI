using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Persons.Service.Dto;
using Persons.Service.Exceptions;
using Persons.Service.Extensions;
using Persons.Service.Models;

namespace Persons.Service.Repositories
{
    /// <summary>
    /// Реализация репозитория на SQLite.
    /// </summary>
    public class PersonRepositorySqLite : IPersonRepository
    {
        /// <summary>
        /// Строковое название репозитория.
        /// </summary>
        private const string repositoryName = "persons";

        /// <summary>
        /// Строка подключения.
        /// </summary>
        private readonly string _connectionString = $"Data Source = {repositoryName}.db3";

        public PersonRepositorySqLite()
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                var checkTableQuery = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{repositoryName}';";
                var result = db.Query(checkTableQuery);

                if (!result.Any())
                    CreateTable(db);
            }

            Console.WriteLine(Resources.Repository_connection_established);
        }

        /// <summary>
        /// Создает таблицу в бд.
        /// </summary>
        private static void CreateTable(IDbConnection db)
        {
            var value = $"CREATE TABLE {repositoryName}(id TEXT PRIMARY KEY, name TEXT, birthday TEXT)";
            db.Query(value);
            Console.WriteLine(value.DefaultFormat());
        }

        /// <inheritdoc />
        public Person Find(Guid id)
        {
            Person person;

            try
            {
                using (IDbConnection db = new SQLiteConnection(_connectionString))
                {
                    var personDto = db.Query($"SELECT id,name,birthday FROM {repositoryName} WHERE Id = @id",
                        new {id = id.ToString()}).FirstOrDefault();

                    if (personDto is null)
                        throw new EntityNotFoundException<Person>();

                    person = Person.Create(Guid.Parse(personDto.id), personDto.name,
                        Converter.ToDateTime(personDto.birthday));
                }
            }
            catch (EntityNotFoundException<Person>)
            {
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Не удалось получить запись из репозитория, ошибка {e.Message}", e);
            }

            return person;
        }
        
        /// <inheritdoc />
        public void Insert(Person item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item),Resources.PersonRepository_Insert_null_person_exception);

            try
            {
                using (IDbConnection db = new SQLiteConnection(_connectionString))
                {
                    var sqlQuery = $"INSERT INTO {repositoryName}(id,name,birthday) VALUES(@id,@name,@birthday)";
                    db.Query(sqlQuery, new
                    {
                        id = item.Id.ToString(),
                        name = item.Name,
                        birthday = item.GetFormattedBirthDay()
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Не удалось добавить запись в репозиторий, ошибка {e.Message}", e);
            }
        }
    }
}