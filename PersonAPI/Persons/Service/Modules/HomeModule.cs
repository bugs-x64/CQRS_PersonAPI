using Nancy;

namespace Persons.Service
{
    /// <summary>
    /// Модуль главной страницы.
    /// </summary>
    public sealed class HomeModule:NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => GetHello());
        }
        
        private static string GetHello() => "hello";
    }
}