﻿using System;
using Topshelf;

namespace Persons
{
    public static class Program
    {
        private static string _host;

        public static void Main()
        {

            HostFactory.Run(x => 
            { 
                x.AddCommandLineDefinition("host", s =>
                {
                    _host = s;
                });
                x.ApplyCommandLine();

                x.Service<StartupService>(s =>
                {
                    s.ConstructUsing(() => new StartupService(_host??"http://127.0.0.1:5000"));
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
