using System.Collections.Generic;

namespace geektrust.Entities
{
    public interface IExtendedRelationships : IRelationships
    {
        IEnumerable<Person> GetPaternalUncles();
        IEnumerable<Person> GetMaternalUncles();
        IEnumerable<Person> GetPaternalAunts();
        IEnumerable<Person> GetMaternalAunts();
        IEnumerable<Person> GetSistersInLaw();
        IEnumerable<Person> GetBrothersInLaw();
        IEnumerable<Person> GetSon();
        IEnumerable<Person> GetDaughter();
        IEnumerable<Person> GetSiblings();
        IEnumerable<Person> GetSpouse();
        IEnumerable<Person> GetChildren();
    }
}