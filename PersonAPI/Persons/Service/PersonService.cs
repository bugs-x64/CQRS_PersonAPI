using Nancy.Hosting.Self;
using System;

namespace Persons.Service
{
    /// <summary>
    /// Http хост сервис.
    /// </summary>
    public class PersonService 
    {
        private readonly string _host;
        private readonly NancyHost _selfHost; 

        public PersonService(string host)
        {
            _host = host;
            _selfHost = new NancyHost(new Uri(host));
        } 

        public void Start() 
        { 
            _selfHost.Start();
            Console.WriteLine($"SelfHost running on {string.Join(",",_host)}");
        } 

        public void Stop() 
        { 
            _selfHost.Stop();
            Console.WriteLine($"SelfHost stopped on {string.Join(",",_host)}");
        } 
    }
}
