namespace Runas.Service
{
    public interface IPasswordService
    {
        void CreateUserPassword(string userName, string password);
        void DeleteUserPassword(string userName, string password);
        void Dispose();
        string GetUserPassword(string userName);
        void UpdateUserPassword(string userName, string password);
    }
}