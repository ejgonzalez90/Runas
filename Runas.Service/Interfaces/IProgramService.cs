using Runas.Service.DataTransferObjects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Runas.Service
{
    public interface IProgramService
    {
        void Runas(
            string program,
            NetworkCredential networkCredential = null,
            bool loadProfile = false,
            bool env = false,
            bool netonly = false,
            bool savecred = true,
            bool smartcard = false,
            string trustlevel = "0x20000"
            );

        void Runas(
            string program,
            string arguments,
            string userName,
            string password);

        Process Runas(
            string program,
            string arguments = null,
            NetworkCredential networkCredential = null
            );

        IEnumerable<ProcessInfo> GetProcesses(string processName = null);
    }
}