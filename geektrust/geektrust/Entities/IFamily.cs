using System.Collections.Generic;

namespace geektrust.Entities
{
    public interface IFamily
    {
        IEnumerable<Person> FindRelatedMembers(string name, string relation);
    }
}