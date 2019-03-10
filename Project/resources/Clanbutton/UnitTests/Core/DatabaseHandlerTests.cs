using Clanbutton.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTests.Core
{
    [TestClass]
    public class DatabaseHandlerTests
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

        private DatabaseHandler CreateDatabaseHandler()
        {
            return new DatabaseHandler();
        }

        [TestMethod]
        public async Task AccountExistsAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateDatabaseHandler();
            string userId = TODO;

            // Act
            var result = await unitUnderTest.AccountExistsAsync(
                userId);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task CreateAccount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateDatabaseHandler();
            string userId = TODO;
            ulong steamId = TODO;
            string userEmail = TODO;

            // Act
            var result = await unitUnderTest.CreateAccount(
                userId,
                steamId,
                userEmail);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetAccountAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateDatabaseHandler();
            string userId = TODO;

            // Act
            var result = await unitUnderTest.GetAccountAsync(
                userId);

            // Assert
            Assert.Fail();
        }
    }
}
