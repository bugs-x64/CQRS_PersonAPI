using System;
using Nancy;
using Nancy.Routing;

namespace Persons.Service
{
    public sealed class PersonsModule:NancyModule
    {
        public PersonsModule():base()
        {
            var basePath = RouteBuilder
                .Builder()
                .Add(RouteConstants.Root)
                .Add(RouteConstants.Version)
                .Add("persons")
                .Route();
            
            Get(basePath + "/hello", _ => GetHello());
            Get("/", _ => GetHello());
        }

        private string GetHello() => "hello";
    }
}