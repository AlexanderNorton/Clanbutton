using Android.Views;
using Clanbutton;
using Clanbutton.Builders;
using Clanbutton.ListViewAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    [TestClass]
    public class ListViewAdapterTests
    {
        private MockRepository mockRepository;

        private Mock<MessagingActivity> mockMessagingActivity;
        private Mock<List<MessageContent>> mockList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockMessagingActivity = this.mockRepository.Create<MessagingActivity>();
            this.mockList = this.mockRepository.Create<List<MessageContent>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private ListViewAdapter CreateListViewAdapter()
        {
            return new ListViewAdapter(
                this.mockMessagingActivity.Object,
                this.mockList.Object);
        }

        [TestMethod]
        public void GetItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateListViewAdapter();
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
            var unitUnderTest = this.CreateListViewAdapter();
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
            var unitUnderTest = this.CreateListViewAdapter();
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
            var unitUnderTest = this.CreateListViewAdapter();
            string userId = TODO;

            // Act
            await unitUnderTest.OpenProfile(
                userId);

            // Assert
            Assert.Fail();
        }
    }
}
