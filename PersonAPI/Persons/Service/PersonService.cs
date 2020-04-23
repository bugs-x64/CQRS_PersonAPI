using Nancy.Hosting.Self;
using System;

namespace Persons.Service
{
    /// <summary>
    /// Http хост сервис.
    /// </summary>
    public sealed class PersonService : IDisposable
    {
        private readonly string _host;
        private readonly NancyHost _selfHost;
        private bool _disposed;
        private readonly DefaultBootstrapper _defaultBootstrapper = new DefaultBootstrapper();

        public PersonService(string host)
        {
            _host = host;
            _selfHost = new NancyHost(new Uri(host),_defaultBootstrapper);
        } 
        ~PersonService()
        {
            Dispose(true);
        } 

        public void Start() 
        { 
            _selfHost.Start();
            Console.WriteLine(Resources.PersonService_Start_SelfHost, string.Join(",",_host));
        } 

        public void Stop() 
        { 
            _selfHost.Stop();
            Console.WriteLine(Resources.PersonService_Stop_SelfHost, string.Join(",",_host));
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing) {
                _selfHost.Stop();
                _selfHost.Dispose();
                _defaultBootstrapper.Dispose();
            }

            _disposed = true;
        }
    }
}
