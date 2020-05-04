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
        private const string correctName = "Name";
        private const string correctDate = "1990-09-09";
        private readonly Guid _personId = Guid.NewGuid();
        private static PersonRepositorySqLite Instance => new PersonRepositorySqLite();
        
        [TestMethod]
        public void Initialize_CorrectPersonData_RunWithoutException()
        {
            var _ = new PersonRepositorySqLite();
        }

        
        [TestMethod]
        public void Insert_CorrectPersonData_RunWithoutException()
        {
            var person = Person.Create(_personId,correctName,Convert.ToDateTime(correctDate));

            Instance.Insert(person);
        }

        [TestMethod]
        public void Insert_NullData_ThrowArgumentNullException()
        {
            void Action() => Instance.Insert(null);

            Assert.ThrowsException<ArgumentNullException>(Action);
        }

        [TestMethod]
        public void Find_CorrectPersonId_ReturnPersonIdEqualsInitial()
        {
            var person = Person.Create(_personId, correctName, Convert.ToDateTime(correctDate));

            Instance.Insert(person);

           var result = Instance.Find(_personId);

           Assert.AreEqual(_personId,result.Id);
        }

        
        [TestMethod]
        public void Find_NonexistentPersonId_ThrowEntityNotFoundException()
        {
           var person = Guid.NewGuid();

           void Action() => Instance.Find(person);

           Assert.ThrowsException<EntityNotFoundException<Person>>(Action);
        }
    }
}