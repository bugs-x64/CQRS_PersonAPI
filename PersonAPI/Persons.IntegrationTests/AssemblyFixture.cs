using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Persons.IntegrationTests
{
    [TestClass]
    public class AssemblyFixture
    {
        private static Process _proc;
        private static readonly string AppName = "Persons";

        [AssemblyInitialize]
        public static void StartApplicationAsync(TestContext qwe)
        {
            _ = RunAsync();
        }

        private static async Task RunAsync()
        {
            Debug.Flush();

            foreach (var process in Process.GetProcessesByName(AppName))
            {
                process.Kill();
            }

            _proc = new Process
            {
                StartInfo =
                {
                    FileName = AppName+".exe",
                    Arguments = $"-host {GlobalParameters.Host}",
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory + "\\..\\..\\..\\persons\\bin\\debug"
                }
            };
            _proc.Start();
        }


        [AssemblyCleanup]
        public static void StopApplication()
        {
            foreach (var process in Process.GetProcessesByName(AppName))
            {
                process.Kill();
            }
        }
    }
}