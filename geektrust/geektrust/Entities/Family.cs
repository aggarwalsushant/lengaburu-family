using System;
using System.Collections.Generic;
using System.Linq;

namespace geektrust.Entities
{
    public class Family : IFamily
    {
        private Family()
        {
            Relations = new Dictionary<string, Relations>
            {
                {"PATERNAL-UNCLE", Entities.Relations.PaternalUncle},
                {"MATERNAL-UNCLE", Entities.Relations.MaternalUncle},
                {"PATERNAL-AUNT" , Entities.Relations.PaternalAunt },
                {"MATERNAL-AUNT" , Entities.Relations.MaternalAunt },
                {"SISTER-IN-LAW" , Entities.Relations.SisterInLaw  },
                {"BROTHER-IN-LAW", Entities.Relations.BrotherInLaw },
                {"SON"           , Entities.Relations.Son          },
                {"DAUGHTER"      , Entities.Relations.Daughter     },
                {"SIBLINGS"      , Entities.Relations.Siblings     }
            };
        }

        public Family(Dictionary<string, Person> members) : this()
        {
            Members = members;
        }

        public Dictionary<string, Person> Members { get; }
        private Dictionary<string, Relations> Relations { get; }

        public IEnumerable<Person> FindRelatedMembers(string name, string relation)
        {
            var relationToFind = Relations.ContainsKey(relation.Cleanse())
                ? Relations[relation.Cleanse()]
                : Entities.Relations.Invalid;

            IEnumerable<Person> members;

            if (Members.TryGetValue(name, out var person))
                members = relationToFind switch
                {
                    Entities.Relations.PaternalUncle => person.Relationships.GetPaternalUncles()                  ,
                    Entities.Relations.MaternalUncle => person.Relationships.GetMaternalUncles()                  ,
                    Entities.Relations.PaternalAunt  => person.Relationships.GetPaternalAunts()                   ,
                    Entities.Relations.MaternalAunt  => person.Relationships.GetMaternalAunts()                   ,
                    Entities.Relations.SisterInLaw   => person.Relationships.GetSistersInLaw()                    ,
                    Entities.Relations.BrotherInLaw  => person.Relationships.GetBrothersInLaw()                   ,
                    Entities.Relations.Son           => person.Relationships.GetSon()                             ,
                    Entities.Relations.Daughter      => person.Relationships.GetDaughter()                        ,
                    Entities.Relations.Siblings      => person.Relationships.GetSiblings().Where(x => x != person),
                    _                                => throw new Exception("INVALID RELATIONSHIP SOUGHT")        ,
                };
            else
                throw new Exception("PERSON_NOT_FOUND");

            return members;
        }
    }
}