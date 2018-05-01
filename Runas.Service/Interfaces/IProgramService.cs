namespace Runas.Service
{
    public interface IProgramService
    {
        void Runas(string user, string cmd);
    }
}