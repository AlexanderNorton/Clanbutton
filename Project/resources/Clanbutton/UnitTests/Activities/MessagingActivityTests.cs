using Clanbutton;
using Firebase.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Activities
{
    [TestClass]
    public class MessagingActivityTests
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

        private MessagingActivity CreateMessagingActivity()
        {
            return new MessagingActivity();
        }

        [TestMethod]
        public void OnCancelled_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateMessagingActivity();
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
            var unitUnderTest = this.CreateMessagingActivity();
            DataSnapshot snapshot = TODO;

            // Act
            unitUnderTest.OnDataChange(
                snapshot);

            // Assert
            Assert.Fail();
        }
    }
}
