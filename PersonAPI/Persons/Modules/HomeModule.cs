using Nancy;
using Persons.Logging;

namespace Persons.Modules
{
    /// <summary>
    /// Модуль главной страницы.
    /// </summary>
    public sealed class HomeModule : NancyModule
    {
        public HomeModule(ILog log)
        {
            Get("/",_=>
            {
                const string hello = "Hello";
                log.Log(LogLevel.Debug, () => hello);

                return hello;
            });
        }
    }
}