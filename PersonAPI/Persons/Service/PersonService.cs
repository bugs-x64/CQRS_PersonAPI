using System;
using Nancy.Hosting.Self;

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

        public PersonService(string host)
        {
            var configuration = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            _host = host;
            _selfHost = new NancyHost(configuration,new Uri(host));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PersonService()
        {
            Dispose(true);
        }

        public void Start()
        {
            _selfHost.Start();
            Console.WriteLine(Resources.PersonService_Start_SelfHost, string.Join(",", _host));
        }

        public void Stop()
        {
            _selfHost.Stop();
            Console.WriteLine(Resources.PersonService_Stop_SelfHost, string.Join(",", _host));
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _selfHost.Stop();
                _selfHost.Dispose();
            }

            _disposed = true;
        }
    }
}