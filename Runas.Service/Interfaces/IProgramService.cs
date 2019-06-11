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
    }
}