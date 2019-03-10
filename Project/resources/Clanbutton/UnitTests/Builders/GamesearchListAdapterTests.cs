using Android.Views;
using Clanbutton.Activities;
using Clanbutton.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    [TestClass]
    public class GamesearchListAdapterTests
    {
        private MockRepository mockRepository;

        private Mock<SearchActivity> mockSearchActivity;
        private Mock<List<GameSearch>> mockList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockSearchActivity = this.mockRepository.Create<SearchActivity>();
            this.mockList = this.mockRepository.Create<List<GameSearch>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private GamesearchListAdapter CreateGamesearchListAdapter()
        {
            return new GamesearchListAdapter(
                this.mockSearchActivity.Object,
                this.mockList.Object);
        }

        [TestMethod]
        public void GetItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateGamesearchListAdapter();
            int position = TODO;

            // Act
            var result = unitUnderTest.GetItem(
                position);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetItemId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateGamesearchListAdapter();
            int position = TODO;

            // Act
            var result = unitUnderTest.GetItemId(
                position);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetView_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateGamesearchListAdapter();
            int position = TODO;
            View convertView = TODO;
            ViewGroup parent = TODO;

            // Act
            var result = unitUnderTest.GetView(
                position,
                convertView,
                parent);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task OpenProfile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateGamesearchListAdapter();
            string userId = TODO;

            // Act
            await unitUnderTest.OpenProfile(
                userId);

            // Assert
            Assert.Fail();
        }
    }
}
