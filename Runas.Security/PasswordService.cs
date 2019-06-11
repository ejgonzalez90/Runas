using Runas.Security.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runas.Security
{
    /// <summary>
    /// It deals with encrypted reading and writing of passwords used for different users
    /// </summary>
    public class PasswordService : IPasswordService
    {
        private const string appFolder = "RunasSecurity";
        private const string appPasswordFileName = "runas.security";        
        private Encoding encoding = Encoding.Unicode;
        private FileStream passwordFileStream;
        private const char separator = '\t';
        // private readonly string enteredPassword;

        private static PasswordService instace;
        private static object instanceLocker = new object { };

        private PasswordService(string enteredPassword)
        {
            // TODO: Usar para desencriptar
            // this.enteredPassword = enteredPassword;

            var passwordsFilePath = GetPasswordsFilePath();
            passwordFileStream = File.Open(passwordsFilePath, FileMode.OpenOrCreate);
        }

        /// <summary>
        /// Gets a singleton instance of PasswordService. This implies reading a file, so 
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <returns></returns>
        public static PasswordService GetInstance(string enteredPassword)
        {
            if (instace == null)
            {
                lock (instanceLocker)
                {
                    if (instace == null)
                    {
                        instace = new PasswordService(enteredPassword);
                    }
                }
            }

            return instace;
        }

        private string GetPasswordsFilePath()
        {
            var userProfileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (!userProfileFolderPath.EndsWith("\\"))
            {
                userProfileFolderPath += "\\";
            }

            if (!Directory.Exists($"{userProfileFolderPath}{appFolder}"))
            {
                Directory.CreateDirectory($"{userProfileFolderPath}{appFolder}");
            }

            return $"{userProfileFolderPath}{appFolder}\\{appPasswordFileName}";
        }

        /// <summary>
        /// Gets the specified user's stored password. If the entered password at the application start is not correct, password decryption won't work.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserPassword(string userName)
        {
            if (userName == null || string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            byte[] passwordFile = new byte[passwordFileStream.Length];
            var result = passwordFileStream.ReadAsync(passwordFile, 0, 5).Result;

            return "";
        }

        /// <summary>
        /// Creates and stores a new password for the specified user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void CreateUserPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing password for the specified user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void UpdateUserPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the password for the specified user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void DeleteUserPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disposes the service and closes all open files associated with it
        /// </summary>
        public void Dispose()
        {
            passwordFileStream.Close();
        }
    }
}
