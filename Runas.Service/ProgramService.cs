using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Runas.Service
{
    public class ProgramService : IProgramService
    {
        public void Runas(
            string program,
            NetworkCredential networkCredential = null,
            bool loadProfile = false,
            bool env = false,
            bool netonly = false,
            bool savecred = true,
            bool smartcard = false,
            string trustlevel = "0x20000"
            )
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("runas");

            if(networkCredential != null)
                processStartInfo.Arguments = string.Format(
                    (loadProfile ? "/profile" : "/noprofile") +
                    (env ? "/env " : string.Empty) + 
                    (netonly ? "/netonly " : string.Empty) +
                    (savecred ? "/savecred " : string.Empty) +
                    (smartcard ? "/smartcard " : string.Empty) +
                    "/user:{0} " +
                    "/trustlevel {1}" +
                    "{2}",
                    networkCredential.UserName,
                    trustlevel,
                    program);

            Process.Start(processStartInfo);
        }

        public void Runas(
            string program,
            string arguments = null,
            NetworkCredential networkCredential = null
            )
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(
                program,
                arguments
                );

            if(networkCredential != null)
            {
                processStartInfo.Domain = networkCredential.Domain;
                processStartInfo.UserName = networkCredential.UserName;
            }

            Process.Start(processStartInfo);
        }
    }
}
