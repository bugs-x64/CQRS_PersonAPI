using System;
using Nancy.Hosting.Self;

namespace Persons
{
    /// <summary>
    /// Http хост сервис.
    /// </summary>
    public sealed class StartupService : IDisposable
    {
        private readonly string _host;
        private readonly NancyHost _selfHost;
        private bool _disposed;

        public StartupService(string host)
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