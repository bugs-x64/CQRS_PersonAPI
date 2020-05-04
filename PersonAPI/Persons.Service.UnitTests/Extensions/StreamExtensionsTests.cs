using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Service.Extensions;

namespace Persons.Service.UnitTests.Extensions
{
    [TestClass]
    public class StreamExtensionsTests
    {
        private readonly Encoding _encoding = Encoding.Default;
        
        [TestMethod]
        public void ReadToString_NotNullString_ReadEqualsTest()
        {
            const string expected = "This is test string";
            var stringBytes = _encoding.GetBytes(expected);
            using (var stream = new MemoryStream(stringBytes))
            {
                var str = stream.ReadToString(_encoding);

                Assert.AreEqual(expected,str);
            }
        }

        [DataTestMethod]
        [DataRow]
        public void ReadToString_Null_ThrowArgumentNullException(Stream stream)
        {
            Assert.ThrowsException<ArgumentNullException>(() => stream.ReadToString(_encoding));
        }
    }
}