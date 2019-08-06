using Runas.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runas.Console
{
    class Program
    {
        private static ProgramService programService = new ProgramService();

        private static IDictionary<string, string> programDictionary = new Dictionary<string, string>
        {
            { "VisualStudio", "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.exe" },
            { "Paint", "C:\\Windows\\system32\\mspaint.exe" },
            { "RemoteDesktop", "C:\\Windows\\system32\\mstsc.exe" }
        };

        static void Main(string[] args)
        {
            var input = string.Empty;

            using (programService)
            {
                while (input != "Salir")
                {
                    System.Console.WriteLine("Elija una opción:\n");
                    foreach (var program in programDictionary)
                    {
                        System.Console.WriteLine(program.Key);
                    }

                    input = System.Console.ReadLine();
                    if (input == "Salir")
                        break;

                    if (input == "Listar")
                    {
                        foreach (var serviceProcess in programService.GetProcesses(null))
                        {
                            System.Console.WriteLine(serviceProcess.ToString());
                            continue;
                        }
                    }

                    var process = programDictionary.SingleOrDefault(p => p.Key == input).Value;
                    if (string.IsNullOrEmpty(process))
                    {
                        System.Console.WriteLine("Opción no válida!");
                    }
                    else
                    {
                        programService.Runas(process, null, null);
                    }
                }
            }

            System.Console.ReadLine();
        }
    }
}
