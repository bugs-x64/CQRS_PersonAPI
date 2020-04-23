using System;
using Nancy;
using Nancy.TinyIoc;
using Persons.Abstractions;

namespace Persons.Service
{
    public class DefaultBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider => new DefaultRootPathProvider();

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            if (container == null) 
                return;

            container.Register<ICommandHandler<CreatePersonCommand, Guid>, CreatePersonCommandHandler>().AsMultiInstance();
            container.Register<IQueryHandler<GetPersonQuery, PersonDto>, GetPersonQueryHandler>().AsMultiInstance();
            container.Register<IPersonRepository, PersonRepository>().AsSingleton();
        }
    }
}