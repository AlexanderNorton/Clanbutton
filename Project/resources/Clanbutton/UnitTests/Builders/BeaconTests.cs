using Clanbutton.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Builders
{
    [TestClass]
    public class BeaconTests
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

        private Beacon CreateBeacon()
        {
            return new Beacon();
        }

        [TestMethod]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateBeacon();

            // Act
            unitUnderTest.Create();

            // Assert
            Assert.Fail();
        }
    }
}
