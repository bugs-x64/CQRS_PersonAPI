using Nancy.Hosting.Self;
using System;
using System.Linq;

namespace Persons.Service
{
    /// <summary>
    /// Http хост сервис.
    /// </summary>
    public class PersonService 
    {
        private readonly string[] _uris;
        private readonly NancyHost _selfHost; 

        public PersonService(params string[] uris)
        {
            _uris = uris;

            var baseUris = uris.Select(uri=>new Uri(uri)).ToArray();
            _selfHost = new NancyHost(baseUris);
        } 

        public void Start() 
        { 
            _selfHost.Start();
            Console.WriteLine($"SelfHost running on {string.Join(",",_uris)}");
        } 

        public void Stop() 
        { 
            _selfHost.Stop();
            Console.WriteLine($"SelfHost stopped on {string.Join(",",_uris)}");
        } 
    }
}
