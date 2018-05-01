using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Runas.Service
{
    public class CredentialsService
    {
        public NetworkCredential GetNetworkCredentials(string userName, string password)
        {
            return new NetworkCredential(userName, password);
        }
    }
}
