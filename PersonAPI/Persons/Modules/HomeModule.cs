using Nancy;
using Persons.Logging;

namespace Persons.Modules
{
    /// <summary>
    /// Модуль главной страницы.
    /// </summary>
    public sealed class HomeModule : NancyModule
    {
        private readonly ILog _log;

        public HomeModule(ILog log)
        {
            _log = log;
            Get("/", _ => GetHome());
        }

        public string GetHome()
        {
            const string hello = "Hello";
            _log.Log(LogLevel.Debug, () => hello);
            return hello;
        }
    }
}