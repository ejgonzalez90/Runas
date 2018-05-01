using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runas.Service;

namespace Runas.Test
{
    [TestClass]
    public class ProgramServiceTest
    {
        ProgramService service;

        public ProgramServiceTest()
        {
            this.service = new ProgramService();
        }

        [TestMethod]
        public void ProgramServiceTest_Runas_RunsProcess()
        {
            string testUser = @"ELEMO-LAPTOP";

            service.Runas(testUser, "cmd");

            var result = Process.GetProcessesByName("runas");
            Assert.IsNotNull(result);
        }
    }
}
