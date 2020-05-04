using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Service.Exceptions;
using Persons.Service.Models;
using Persons.Service.Repositories;

namespace Persons.Service.UnitTests.Repositories
{
    [TestClass]
    public class PersonRepositorySqLiteTests
    {
        private PersonRepositorySqLite _instance;

        [TestInitialize]
        public void Initialize()
        {
            _instance = new PersonRepositorySqLite();
        }
        
        [TestMethod]
        public void Insert_CorrectPersonData_RunWithoutException()
        {
            var name = "Name";
            var birthDay = "1977-07-07";
            var person = Person.Create(Guid.NewGuid(),name,Convert.ToDateTime(birthDay));

            _instance.Insert(person);
        }

        [TestMethod]
        public void Insert_NullData_ThrowArgumentNullException()
        {
            void Action() => _instance.Insert(null);

            Assert.ThrowsException<ArgumentNullException>(Action);
        }

        [TestMethod]
        public void Find_CorrectPersonId_ReturnPersonIdEqualsInitial()
        {
            var personId = Guid.NewGuid();
            var name = "Name";
            var birthDay = "1977-07-07";
            var person = Person.Create(personId, name, Convert.ToDateTime(birthDay));

            _instance.Insert(person);

           var result = _instance.Find(personId);

           Assert.AreEqual(personId,result.Id);
        }

        
        [TestMethod]
        public void Find_NonexistentPersonId_ThrowEntityNotFoundException()
        {
           var person = Guid.NewGuid();

           void Action() => _instance.Find(person);

           Assert.ThrowsException<EntityNotFoundException<Person>>(Action);
        }
    }
}