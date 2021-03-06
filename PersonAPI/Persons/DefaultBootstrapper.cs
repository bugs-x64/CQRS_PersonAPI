﻿using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;
using Persons.Abstractions;
using Persons.Abstractions.Commands;
using Persons.Abstractions.Queries;
using Persons.Handlers;
using Persons.Logging;
using Persons.Service.Dto;
using Persons.Service.Repositories;
using Serilog;

namespace Persons
{
    /// <summary>
    /// Стандартный DI Nancy.
    /// </summary>
    /// <remarks>
    ///  Автоматический DI (так называемый Super-Duper-Happy-Path) не сработал. Пришлось регистрировать вручную.
    /// </remarks>
    public class DefaultBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider => new DefaultRootPathProvider();

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            if (container == null)
                return;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            container.Register<ICommandHandler<CreatePersonCommand, Guid>, CreatePersonCommandHandler>().AsMultiInstance();
            container.Register<IQueryHandler<GetPersonQuery, PersonDto>, GetPersonQueryHandler>().AsMultiInstance();
            container.Register<IPersonRepository, PersonRepositorySqLite>().AsSingleton();
            container.Register(LogProvider.GetCurrentClassLogger());
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