using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Runas.Service;

namespace Runas.Test
{
    [TestClass]
    public class PasswordServiceTests
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

        private PasswordService CreateService()
        {
            return PasswordService.GetInstance("");
        }

        [TestMethod]
        public void GetInstance_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            string enteredPassword = "";

            // Act
            var result = PasswordService.GetInstance(enteredPassword);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetUserPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            string userName = "";

            // Act
            var result = unitUnderTest.GetUserPassword(
                userName);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CreateUserPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            string userName = "";
            string password = "";

            // Act
            unitUnderTest.CreateUserPassword(
                userName,
                password);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateUserPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            string userName = "";
            string password = "";

            // Act
            unitUnderTest.UpdateUserPassword(
                userName,
                password);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteUserPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            string userName = "";
            string password = "";

            // Act
            unitUnderTest.DeleteUserPassword(
                userName,
                password);

            // Assert
            Assert.Fail();
        }
    }
}
