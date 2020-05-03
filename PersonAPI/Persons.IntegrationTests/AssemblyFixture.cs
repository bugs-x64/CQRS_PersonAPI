using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Persons.IntegrationTests
{
    [TestClass]
    public class AssemblyFixture
    {
        private static readonly string AppName = "Persons";

        [AssemblyInitialize]
        public static void StartApplicationAsync(TestContext qwe)
        {
            foreach (var process in Process.GetProcessesByName(AppName))
            {
                process.Kill();
            }

            var proc = new Process
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

            proc.Start();
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