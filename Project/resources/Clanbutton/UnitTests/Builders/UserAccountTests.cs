using Clanbutton.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    [TestClass]
    public class UserAccountTests
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

        private UserAccount CreateUserAccount()
        {
            return new UserAccount();
        }

        [TestMethod]
        public async Task Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateUserAccount();

            // Act
            await unitUnderTest.Update();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task FillSteamData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateUserAccount();

            // Act
            await unitUnderTest.FillSteamData();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void IsFollowing_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateUserAccount();
            UserAccount account = TODO;

            // Act
            var result = unitUnderTest.IsFollowing(
                account);

            // Assert
            Assert.Fail();
        }
    }
}
