using Android.Views;
using Clanbutton.Activities;
using Clanbutton.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Steam.Models.SteamCommunity;
using System;
using System.Collections.Generic;

namespace UnitTests.Builders
{
    [TestClass]
    public class LibraryGridAdapterTests
    {
        private MockRepository mockRepository;

        private Mock<SearchActivity> mockSearchActivity;
        private Mock<List<OwnedGameModel>> mockList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockSearchActivity = this.mockRepository.Create<SearchActivity>();
            this.mockList = this.mockRepository.Create<List<OwnedGameModel>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private LibraryGridAdapter CreateLibraryGridAdapter()
        {
            return new LibraryGridAdapter(
                this.mockSearchActivity.Object,
                this.mockList.Object);
        }

        [TestMethod]
        public void GetItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateLibraryGridAdapter();
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
            var unitUnderTest = this.CreateLibraryGridAdapter();
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
            var unitUnderTest = this.CreateLibraryGridAdapter();
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
    }
}
