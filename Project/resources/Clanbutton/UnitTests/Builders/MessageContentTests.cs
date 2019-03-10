using Clanbutton.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Builders
{
    [TestClass]
    public class MessageContentTests
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

        private MessageContent CreateMessageContent()
        {
            return new MessageContent();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateMessageContent();

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
