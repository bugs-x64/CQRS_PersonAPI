using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persons.Abstractions;
using Persons.Service;
using Topshelf;
using Topshelf.Runtime;

namespace Persons
{
    class Program
    {
        private static string _hosts;

        static void Main(string[] args)
        {

            HostFactory.Run(x => 
            { 
                x.AddCommandLineDefinition("hosts", s=> { _hosts = s; });
                x.ApplyCommandLine();

                x.Service<PersonService>(s =>
                {
                    s.ConstructUsing(() => new PersonService(_hosts.Split(' ')));
                    s.WhenStarted(svc => svc.Start()); 
                    s.WhenStopped(svc => svc.Stop()); 
                }); 

                x.RunAsLocalSystem(); 
                x.SetDescription("PersonService CQRS API selfhosting Windows Service"); 
                x.SetDisplayName("PersonService CQRS API"); 
                x.SetServiceName("PersonService"); 
            }); 
            Console.ReadKey(true);
        }
    }
}
