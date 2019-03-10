using Clanbutton.Activities;
using Firebase.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTests.Activities
{
    [TestClass]
    public class SearchActivityTests
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

        private SearchActivity CreateSearchActivity()
        {
            return new SearchActivity();
        }

        [TestMethod]
        public async Task StartSearching_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSearchActivity();
            string current_game = TODO;

            // Act
            await unitUnderTest.StartSearching(
                current_game);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task RefreshPlayers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSearchActivity();

            // Act
            await unitUnderTest.RefreshPlayers();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OnCancelled_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSearchActivity();
            DatabaseError error = TODO;

            // Act
            unitUnderTest.OnCancelled(
                error);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void OnDataChange_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSearchActivity();
            DataSnapshot snapshot = TODO;

            // Act
            unitUnderTest.OnDataChange(
                snapshot);

            // Assert
            Assert.Fail();
        }
    }
}
