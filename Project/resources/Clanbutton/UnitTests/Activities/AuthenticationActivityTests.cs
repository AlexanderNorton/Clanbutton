using Android.Gms.Tasks;
using Android.Views;
using Clanbutton.Activities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTests.Activities
{
    [TestClass]
    public class AuthenticationActivityTests
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

        private AuthenticationActivity CreateAuthenticationActivity()
        {
            return new AuthenticationActivity();
        }

        [TestMethod]
        public void OnKeyDown_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateAuthenticationActivity();
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
        public async Task SteamAuth_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateAuthenticationActivity();
            ulong userid = TODO;

            // Act
            await unitUnderTest.SteamAuth(
                userid);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task OnComplete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateAuthenticationActivity();
            Task task = TODO;

            // Act
            await unitUnderTest.OnComplete(
                task);

            // Assert
            Assert.Fail();
        }
    }
}
