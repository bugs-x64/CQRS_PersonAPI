using System;
using Autofac;
using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.Abstractions.Queries;
using Persons.Handlers;
using Persons.Logging;
using Persons.Service.Dto;
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
            
            var builder = new ContainerBuilder();
 
            builder.RegisterType<CreatePersonCommandHandler>().As<ICommandHandler<CreatePersonCommand, Guid>>();
            builder.RegisterType<GetPersonQueryHandler>().As<IQueryHandler<GetPersonQuery, PersonDto>>();
            builder.RegisterType<PersonRepositorySqLite>().As<IPersonRepository>();
            builder.RegisterType<CreatePersonCommandHandler>().As<ICommandHandler<CreatePersonCommand, Guid>>();
            builder.RegisterInstance(LogProvider.GetCurrentClassLogger());

           return  builder.Build();
        }
    }
}