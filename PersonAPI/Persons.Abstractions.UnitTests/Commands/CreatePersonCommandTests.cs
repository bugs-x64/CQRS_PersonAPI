using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions.Commands;

namespace Persons.Abstractions.UnitTests.Commands
{
    [TestClass]
    public class CreatePersonCommandTests
    {
        private const string correctName = "Test";
        private const string correctBirthDay = "1977-07-07";

        [TestMethod]
        public void Initialization_CorrectNameAndDate_CreateWithoutException()
        {
            var _ = new CreatePersonCommand(correctName, correctBirthDay);
        }

        [DataTestMethod]
        [DataRow(null, correctBirthDay)]
        [DataRow(correctName, null)]
        [DataRow]
        public void Initialization_NullNameOrDate_ThrowArgumentNullException(string name = null, string birthday = null)
        {
            void Action() => new CreatePersonCommand(name, birthday);

            Assert.ThrowsException<ArgumentNullException>(Action);
        }
    }
}