using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Runas.Service;
using System;
using System.Linq;
using System.Net;

namespace Runas.Test
{
    [TestClass]
    public class ProgramServiceTests
    {
        private MockRepository mockRepository;

        private ProgramService programService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            programService = new ProgramService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();

            programService.Dispose();
        }

        [TestMethod]
        public void ProgramService_Runas_WhenAValidSetOfConsoleParametersIsSentAValidProcessIsReturned()
        {
            // Arrange            
            string program = null;
            NetworkCredential networkCredential = null;
            bool loadProfile = false;
            bool env = false;
            bool netonly = false;
            bool savecred = false;
            bool smartcard = false;
            string trustlevel = null;

            // Act
            programService.Runas(
                program,
                networkCredential,
                loadProfile,
                env,
                netonly,
                savecred,
                smartcard,
                trustlevel);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProgramService_Runas_WhenANullProgramUserNameOrPasswordIsSpecified_ArgumentNullExceptionIsThrown()
        {
            // Arrange
            string program = null;
            string arguments = null;
            string userName = null;
            string password = null;

            // Act
            programService.Runas(
                program,
                arguments,
                userName,
                password);
        }

        [TestMethod]
        public void ProgramService_Runas_WhenAValidSetOfClassParametersIsSentAValidProcessIsReturned()
        {
            // Arrange
            
            string program = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.exe";
            string arguments = null;
            string userName = string.Empty;
            string password = string.Empty;

            // Act
            programService.Runas(
                    program,
                    arguments,
                    userName,
                    password);

            Assert.IsTrue(programService.Processes.Count == 1);
        }

        [TestMethod]
        public void ProgramService_Runas_WhenAValidNetworkCredentialIsSentAValidProcessIsReturned()
        {
            // Arrange

            string program = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.exe";
            string arguments = null;
            NetworkCredential networkCredential = new NetworkCredential();

            // Act
            programService.Runas(
                    program,
                    arguments,
                    networkCredential);

            Assert.IsTrue(programService.Processes.Count == 1);            
        }
    }
}
