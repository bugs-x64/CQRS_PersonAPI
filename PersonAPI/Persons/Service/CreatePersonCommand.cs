using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// Создать запись о личности.
    /// </summary>
    public class CreatePersonCommand : ICommand
    {
        private readonly Person _person;
        private readonly IPersonRepository _repository;

        public CreatePersonCommand(Person person, IPersonRepository repository)
        {
            _person = person;
            _repository = repository;
        }

        public void Execute()
        {
            if (_person.Id == Guid.Empty.ToString())
                _person.Id = Guid.NewGuid().ToString();

            _repository.Insert(_person);
        }
    }
}