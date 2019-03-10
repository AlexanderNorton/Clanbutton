using Clanbutton.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTests.Core
{
    [TestClass]
    public class CacheManagerTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private CacheManager CreateManager()
        {
            return new CacheManager();
        }

        [TestMethod]
        public void Set_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateManager();
            string key = TODO;
            T value = TODO;
            DateTimeOffset absoluteExpiry = TODO;

            // Act
            unitUnderTest.Set(
                key,
                value,
                absoluteExpiry);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateManager();
            string key = TODO;

            // Act
            var result = unitUnderTest.Get(
                key);

            // Assert
            Assert.Fail();
        }
    }
}
