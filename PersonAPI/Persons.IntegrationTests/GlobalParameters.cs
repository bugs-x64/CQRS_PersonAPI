namespace Persons.IntegrationTests
{
    public static class GlobalParameters
    {
        public static string Host { get; }= "http://localhost:5000";
        public static int RequestTimeoutSeconds { get; } = 2;
        public const int TestTimeoutMilliseconds = 155*1000;
        public static string ContentTypeHeader { get; } = "content-type";
        public static string DefaultContentType { get; } = "application/json";
    }
}