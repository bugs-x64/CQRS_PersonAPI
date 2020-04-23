using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Persons.Abstractions;
namespace Persons.Service
{
    public class PersonRepository : IPersonRepository
    {
        private const string repositoryName = "persons";
        private readonly string _connectionString = $"Data Source = {repositoryName}.db3";

        public PersonRepository()
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                var checkTableQuery = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{repositoryName}';";
                var result = db.Query(checkTableQuery);
                if (!result.Any())
                {
                    var value = $"CREATE TABLE {repositoryName}(id TEXT PRIMARY KEY, name TEXT, birthday TEXT)";
                    db.Query(value);
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    Console.WriteLine(value);
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                }
            }

            Console.WriteLine(Resources.Repository_connection_established);
        }

        public Person Find(Guid id)
        {
            // todo проверить, нужно ли здесь генерить ошибку
            Person person;
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                person = db.Query<Person>($"SELECT id,name,birthday FROM {repositoryName} WHERE Id = @id", new { id = id.ToString()}).FirstOrDefault();
            }

            return person;
        }

        public void Insert(Person item)
        {
            if(item is null)
                throw  new ArgumentException(Resources.PersonRepository_Insert_null_person_exception, nameof(item));
            try
            {
                using (IDbConnection db = new SQLiteConnection(_connectionString))
                {
                    var sqlQuery = $"INSERT INTO {repositoryName}(id,name,birthday) VALUES(@id,@name,@birthday)";
                    db.Query(sqlQuery,new
                    {
                        id = item.Id,
                        name = item.Name,
                        birthday = item.BirthDay
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Не удалось добавить запись в репозиторий, ошибка {e.Message}",e);
            }
        }
    }
}