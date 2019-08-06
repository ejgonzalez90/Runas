using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Runas.Service
{
    /// <summary>
    /// It deals with encrypted reading and writing of passwords used for different users
    /// </summary>
    public class PasswordService : IPasswordService, IDisposable
    {
        private const string appFolder = "RunasSecurity";
        private const string appPasswordFileName = "runas.security";

        private readonly Encoding encoding = Encoding.Default;

        private FileStream passwordFileStream;

        private const char valueSeparator = '\t';
        private const char lineSeparator = '\n';
        // private readonly string enteredPassword;

        private static PasswordService instace;
        private static readonly object instanceLocker = new object { };

        private PasswordService(string enteredPassword, Encoding encoding)
        {
            // TODO: Usar para desencriptar
            // this.enteredPassword = enteredPassword;
            if (encoding != null)
            {
                this.encoding = encoding;
            }

            var passwordsFilePath = GetPasswordsFilePath();
            passwordFileStream = File.Open(passwordsFilePath, FileMode.OpenOrCreate);
        }

        /// <summary>
        /// Gets a singleton instance of PasswordService. This implies reading a file, so you need to manage service's disposal.
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <returns></returns>
        public static PasswordService GetInstance(string enteredPassword) =>
            GetInstance(
                enteredPassword,
                null
                );

        /// <summary>
        /// Gets a singleton instance of PasswordService. This implies reading a file, so you need to manage service's disposal.
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static PasswordService GetInstance(string enteredPassword, Encoding encoding)
        {
            if (instace == null)
            {
                lock (instanceLocker)
                {
                    if (instace == null)
                    {
                        instace = new PasswordService(enteredPassword, encoding);
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
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            string passwordFileAsString = GetPasswordFileAsString();

            var result = passwordFileAsString
                .Split(lineSeparator)
                .SingleOrDefault(x => x.Split(valueSeparator)[0] == userName)
                ?.Split(valueSeparator)[1];

            return result;
        }

        private string GetPasswordFileAsString()
        {
            if (passwordFileStream.Length <= 0)
            {
                return null;
            }

            string result = null;

            byte[] passwordFile = new byte[passwordFileStream.Length];

            int read = 0;
            if (passwordFileStream.CanRead)
            {
                read = passwordFileStream.Read(passwordFile, 0, (int)passwordFileStream.Length);
                result = encoding.GetString(passwordFile);
            }

            if (read <= 0)
            {
                throw new Exception();
            }

            return result;
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
