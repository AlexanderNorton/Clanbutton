using Android.Views;
using Clanbutton.Activities;
using Clanbutton.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTests.Activities
{
    [TestClass]
    public class ProfileActivityTests
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

        private ProfileActivity CreateProfileActivity()
        {
            return new ProfileActivity();
        }

        [TestMethod]
        public void OnKeyDown_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateProfileActivity();
            Keycode keyCode = TODO;
            KeyEvent e = TODO;

            // Act
            var result = unitUnderTest.OnKeyDown(
                keyCode,
                e);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SetProfileAccount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateProfileActivity();
            UserAccount Account = TODO;

            // Act
            unitUnderTest.SetProfileAccount(
                Account);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SaveProfileChanges_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateProfileActivity();

            // Act
            unitUnderTest.SaveProfileChanges();

            // Assert
            Assert.Fail();
        }
    }
}
