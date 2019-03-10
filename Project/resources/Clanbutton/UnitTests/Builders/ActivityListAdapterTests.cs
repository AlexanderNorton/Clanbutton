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
    public class ActivityListAdapterTests
    {
        private MockRepository mockRepository;

        private Mock<MainActivity> mockMainActivity;
        private Mock<List<UserActivity>> mockList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockMainActivity = this.mockRepository.Create<MainActivity>();
            this.mockList = this.mockRepository.Create<List<UserActivity>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private ActivityListAdapter CreateActivityListAdapter()
        {
            return new ActivityListAdapter(
                this.mockMainActivity.Object,
                this.mockList.Object);
        }

        [TestMethod]
        public void GetItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateActivityListAdapter();
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
            var unitUnderTest = this.CreateActivityListAdapter();
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
            var unitUnderTest = this.CreateActivityListAdapter();
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
            var unitUnderTest = this.CreateActivityListAdapter();
            string userId = TODO;

            // Act
            await unitUnderTest.OpenProfile(
                userId);

            // Assert
            Assert.Fail();
        }
    }
}
