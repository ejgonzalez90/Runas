using System;

namespace Runas.Security.Interfaces
{
    public interface IPasswordService : IDisposable
    {
        void CreateUserPassword(string userName, string password);
        void DeleteUserPassword(string userName, string password);
        string GetUserPassword(string userName);
        void UpdateUserPassword(string userName, string password);
    }
}