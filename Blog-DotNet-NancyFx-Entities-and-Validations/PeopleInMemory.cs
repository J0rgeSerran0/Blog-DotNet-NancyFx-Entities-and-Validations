namespace Blog_DotNet_NancyFx_Entities_and_Validations
{

    using System.Collections.Generic;
    using System.Linq;
    using Blog_DotNet_NancyFx_Entities_and_Validations.Entities;

    public class PeopleInMemory
    {
        private object _lock = new object();

        private List<PersonEntity> _people;

        public PeopleInMemory()
        {
            _people = new List<PersonEntity>();
        }

        public void RebuildLocalizations()
        {
            lock (_lock)
            {
                _people = new List<PersonEntity>();
            }
        }

        public void Add(PersonEntity personEntity)
        {
            lock (_lock)
            {
                _people.Add(personEntity);
            }
        }

        public List<PersonEntity> GetAll()
        {
            return _people.ToList();
        }

    }

}