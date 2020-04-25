using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Persons.IntegrationTests
{
    [TestClass]
    public class FixtureApp
    {
        private static Process _proc;
        private static CancellationToken _token;

        [AssemblyInitialize]
        public static void StartApplicationAsync(TestContext qwe)
        {
            _ = RunAsync();
        }

        private static async Task RunAsync()
        {
            var appName = "Persons";

            foreach (var process in Process.GetProcessesByName(appName))
            {
                process.Kill();
            }

            _proc = new Process
            {
                StartInfo =
                {
                    FileName = appName+".exe",
                    Arguments = $"-host {GlobalParameters.Host}",
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory + "\\..\\..\\..\\persons\\bin\\debug"
                }
            };
            _proc.Start();

            while (!_token.IsCancellationRequested)
            {
                await Task.Delay(100);
            }

            _proc.Kill();
        }


        [AssemblyCleanup]
        public static void StopApplication()
        {
            _token = new CancellationToken(true);
        }
    }
}