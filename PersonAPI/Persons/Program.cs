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
        private static string _host;

        static void Main(string[] args)
        {

            HostFactory.Run(x => 
            { 
                x.AddCommandLineDefinition("host", s =>
                {
                    _host = s;
                });
                x.ApplyCommandLine();

                x.Service<PersonService>(s =>
                {
                    s.ConstructUsing(() => new PersonService(_host??"http://127.0.0.1:5000"));
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
