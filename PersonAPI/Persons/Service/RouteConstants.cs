using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Service
{
    public static class RouteConstants
    {
        public const string Root = "api";
        public const string Version = "v1";
    }

    public class RouteBuilder
    {
        private readonly List<string> _pathParts = new List<string>();

        private RouteBuilder()
        {
        }
        
        public static RouteBuilder Builder()=>new RouteBuilder();

        public RouteBuilder Add(string path)
        {
            _pathParts.Add(path);
            return this;
        }

        public string Route()
        {
            return _pathParts.Aggregate(string.Empty, (current, part) => current + ('/' + part.Trim().Trim('/')));
        }

    }
}
