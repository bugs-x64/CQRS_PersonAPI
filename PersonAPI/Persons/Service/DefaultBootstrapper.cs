using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;
using Persons.Abstractions;
using Persons.Logging;
using Persons.Service.Commands;
using Persons.Service.Dto;
using Persons.Service.Queries;
using Persons.Service.Repositories;
using Persons.Logging.LogProviders;
using Serilog;

namespace Persons.Service
{
    public class DefaultBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider => new DefaultRootPathProvider();

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            if (container == null)
                return;

            Log.Logger=new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider()); 

            container.Register<ICommandHandler<CreatePersonCommand, Guid>, CreatePersonCommandHandler>().AsMultiInstance();
            container.Register<IQueryHandler<GetPersonQuery, PersonDto>, GetPersonQueryHandler>().AsMultiInstance();
            container.Register<IPersonRepository, PersonRepository>().AsSingleton();
            container.Register(LogProvider.GetLogger("Serilog"));
        }
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                var processors = new[]
                {
                    typeof(JsonProcessor),
                    typeof(ResponseProcessor)
                };

                return NancyInternalConfiguration.WithOverrides(x => x.ResponseProcessors = processors);
            }
        }
    }
}