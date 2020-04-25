using Nancy;

namespace Persons
{
    /// <summary>
    /// Модуль главной страницы.
    /// </summary>
    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => GetHello());
        }

        private static string GetHello()
        {
            return "hello";
        }
    }
}