using Android.Widget;
using Clanbutton.Builders;
using Clanbutton.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTests.Core
{
    [TestClass]
    public class ExtensionMethodsTests
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

        private ExtensionMethods CreateExtensionMethods()
        {
            return new ExtensionMethods();
        }

        [TestMethod]
        public void OpenUserProfile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateExtensionMethods();
            UserAccount Account = TODO;
            Android.Content.Context context = TODO;

            // Act
            unitUnderTest.OpenUserProfile(
                Account,
                context);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DownloadPicture_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateExtensionMethods();
            string url = TODO;
            ImageView imageview = TODO;

            // Act
            unitUnderTest.DownloadPicture(
                url,
                imageview);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void StartCacheManager_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateExtensionMethods();

            // Act
            unitUnderTest.StartCacheManager();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetTimeSince_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateExtensionMethods();
            DateTime dateFrom = TODO;

            // Act
            var result = unitUnderTest.GetTimeSince(
                dateFrom);

            // Assert
            Assert.Fail();
        }
    }
}
