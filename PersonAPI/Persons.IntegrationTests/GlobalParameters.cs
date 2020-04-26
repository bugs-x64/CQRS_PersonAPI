namespace Persons.IntegrationTests
{
    public static class GlobalParameters
    {
        public static string Host { get; }= "http://localhost:5000";

        /// <summary>
        /// Максимальное время выполнения запроса.
        /// </summary>
        public static int RequestTimeoutSeconds { get; } = 2;

        /// <summary>
        /// Максимальное время выполнения кейса.
        /// </summary>
        public const int TestTimeoutMilliseconds = 5000;

        //public static string ContentTypeHeader { get; } = "content-type";
        //public static string DefaultContentType { get; } = "application/json";
    }
}