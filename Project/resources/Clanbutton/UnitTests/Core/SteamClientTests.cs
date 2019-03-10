using Clanbutton.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTests.Core
{
    [TestClass]
    public class SteamClientTests
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

        private SteamClient CreateSteamClient()
        {
            return new SteamClient();
        }

        [TestMethod]
        public async Task GetPlayerSummaryAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSteamClient();
            ulong userId = TODO;

            // Act
            var result = await unitUnderTest.GetPlayerSummaryAsync(
                userId);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetPlayerOwnedGamesAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSteamClient();
            ulong userId = TODO;

            // Act
            var result = await unitUnderTest.GetPlayerOwnedGamesAsync(
                userId);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetFriendModels_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSteamClient();
            ulong userId = TODO;

            // Act
            var result = await unitUnderTest.GetFriendModels(
                userId);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetRecentlyPlayed_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSteamClient();
            ulong userId = TODO;

            // Act
            var result = await unitUnderTest.GetRecentlyPlayed(
                userId);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetAllSteamGames_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSteamClient();

            // Act
            var result = await unitUnderTest.GetAllSteamGames();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetUserCurrentGame_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateSteamClient();
            ulong userId = TODO;

            // Act
            var result = await unitUnderTest.GetUserCurrentGame(
                userId);

            // Assert
            Assert.Fail();
        }
    }
}
