using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persons.Abstractions;
using Persons.Logging;
using Persons.Logging.LogProviders;
using Persons.Service.Commands;
using Persons.Service.Dto;
using Persons.Service.Queries;
using Persons.Service.Repositories;
using Serilog;

namespace Persons.UnitTests
{
    public class Fixture
    {
        public static IContainer RegisterTypes()
        {
            Log.Logger=new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider()); 

            var builder = new ContainerBuilder();
 
            builder.RegisterType<CreatePersonCommandHandler>().As<ICommandHandler<CreatePersonCommand, Guid>>();
            builder.RegisterType<GetPersonQueryHandler>().As<IQueryHandler<GetPersonQuery, PersonDto>>();
            builder.RegisterType<PersonRepositorySqLite>().As<IPersonRepository>();
            builder.RegisterType<CreatePersonCommandHandler>().As<ICommandHandler<CreatePersonCommand, Guid>>();
            builder.RegisterInstance(LogProvider.GetLogger("Serilog"));

           return  builder.Build();
        }
    }
}