using Runas.Service.DataTransferObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace Runas.Service
{
    /// <summary>
    /// Abstracts the use of the System.Diagnostics.Process class.
    /// </summary>
    public class ProgramService : IProgramService, IDisposable
    {
        private bool isDisposed = false;

        /// <summary>
        /// Gets a list of running processes started with this class.
        /// </summary>
        public BlockingCollection<Process> Processes { get; private set; }

        /// <summary>
        /// Kills all the processes asossiated with this ProgramService
        /// </summary>
        ~ProgramService()
        {
            // TODO: Resolver el destructor, parece que al no disposear implícita o explicitamente se deberían matar los procesos de todas formas.
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            // TODO: Resolver el cierre en paralelo de todos los procesos usando Parallel.ForEach
            if (disposing)
            {
                isDisposed = true;

                foreach (var process in Processes)
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                        process.WaitForExit();

                        // TODO: Quitar proceso de la coleccion
                    }

                    process.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns all processes matching the specified name.
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public IEnumerable<ProcessInfo> GetProcesses(string processName = null)
        {
            IEnumerable<ProcessInfo> result = new List<ProcessInfo>();

            if (Processes == null)
            {
                return result;
            }

            result = Processes
                .Where(p => processName == null || p.ProcessName == processName)
                .Select(p => new ProcessInfo
                {
                    Id = p.Id,
                    ProcessName = p.ProcessName
                });

            return result;
        }

        /// <summary>
        /// Runs the specified program using the windows command "runas".
        /// </summary>
        /// <param name="program"></param>
        /// <param name="networkCredential"></param>
        /// <param name="loadProfile"></param>
        /// <param name="env"></param>
        /// <param name="netonly"></param>
        /// <param name="savecred"></param>
        /// <param name="smartcard"></param>
        /// <param name="trustlevel"></param>
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
            if (string.IsNullOrEmpty(program))
            {
                throw new ArgumentNullException("program");
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo("runas");

            if (networkCredential != null)
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

        /// <summary>
        /// Runs a program trying to build a NetworkCredential object with the specified user and password.
        /// </summary>
        /// <param name="program"></param>
        /// <param name="arguments"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void Runas(string program, string arguments, string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            NetworkCredential networkCredential = null;
            if (!string.IsNullOrEmpty(userName))
            {
                networkCredential = new NetworkCredential(userName, password);
            }

            Runas(program,
                arguments,
                networkCredential
                );
        }

        /// <summary>
        /// Runs a program using Process.Start with the specified parameters. If no NetworkCredential is specified, a default one is created.
        /// </summary>
        /// <param name="program"></param>
        /// <param name="arguments"></param>
        /// <param name="networkCredential"></param>
        /// <returns></returns>
        public Process Runas(
            string program,
            string arguments = null,
            NetworkCredential networkCredential = null
            )
        {
            if (string.IsNullOrEmpty(program))
            {
                throw new ArgumentNullException("program");
            }

            if (networkCredential == null)
            {
                networkCredential = new NetworkCredential();
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo(program, arguments);
            processStartInfo.UseShellExecute = false;
            if (networkCredential != null)
            {
                processStartInfo.Domain = networkCredential.Domain;
                processStartInfo.UserName = networkCredential.UserName;
                processStartInfo.Password = networkCredential.SecurePassword;
            }

            // Start the processes with the provided credentials
            var result = Process.Start(processStartInfo);

            // Attach events so they can be logged
            result.EnableRaisingEvents = true;
            result.Exited += new EventHandler(ProcessExited);

            // Add this Process instance to a internal collection
            if (Processes == null)
            {
                Processes = new BlockingCollection<Process>();
            }
            Processes.Add(result);

            return result;
        }

        /// <summary>
        /// Checks if the current instance is disposing, if it is then exits doing nothing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessExited(object sender, EventArgs e)
        {
            if (!isDisposed)
            {
                Console.WriteLine($"[{((Process)sender).Id}] exited.");
            }
        }
    }
}
