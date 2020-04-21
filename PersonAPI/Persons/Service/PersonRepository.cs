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
                    var sqlQuery = $"CREATE TABLE {repositoryName}(id TEXT PRIMARY KEY, name TEXT, birthdate TEXT)";
                    db.Query(sqlQuery);
                    Console.WriteLine(sqlQuery);
                }
            }

            Console.WriteLine("PersonRepository connection established!");
        }

        public Person Find(Guid id)
        {
            Person person;
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                person = db.Query<Person>($"SELECT id,name,birthdate FROM {repositoryName} WHERE Id = @id", new { id = id.ToString()}).FirstOrDefault();
            }

            return person;
        }

        public void Insert(Person item)
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO {repositoryName}(id,name,birthdate) VALUES(@id,@name,@birthdate)";
                db.Query(sqlQuery,new
                {
                    id = item.Id,
                    name = item.Name,
                    birthdate = item.BirthDate
                });
            }
        }
    }
}