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
        ICredentials credentials;

        public void Runas(string user, string cmd)
        {
            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo("runas");
            if(user != null)
                processStartInfo.Arguments = string.Format("/noprofile /savedcred /user:{0} {1}", user, cmd);
            process.StartInfo = processStartInfo;
            process.Start();
        }
    }
}
