using System;
using System.Collections.Generic;
using System.Linq;

namespace geektrust.Entities
{
    public class Relationships : IExtendedRelationships
    {
        public const int SpouseLimit = 1;
        public const int ParentLimit = 2;

        public IList<Person> Spouse { get; private set; }
        public IList<Person> Parents { get; private set; }
        public IList<Person> Children { get; private set; }

        public IList<Person> Siblings { get; private set; }

        private Person Father => Parents?.Where(x => x.Gender == Gender.Male).First();
        private Person Mother => Parents?.Where(x => x.Gender == Gender.Female).First();

        public void AddSibling(Person person)
        {
            Siblings ??= new List<Person>();
            if (!Siblings.Contains(person))
            {
                Siblings.Add(person);
            }
            else
                throw new Exception("Sibling already present");
        }

        public void AddSpouse(Person person)
        {
            Spouse ??= new List<Person>(SpouseLimit);

            if (Spouse.Count == 1)
                throw new Exception("Spouse already present.");

            Spouse.Add(person);
        }

        public void AddParent(Person person)
        {
            if (Parents == null)
            {
                Parents = new List<Person>(ParentLimit)
                {
                    person
                };
            }
            else if (Parents.Count < ParentLimit && !Parents.Contains(person))
            {
                // Same gender parents are allowed now
                //if (Parents.Any(x => x.Gender == person.Gender))
                //    throw new Exception("same gender Parents are disallowed.");

                Parents.Add(person);
            }
            else
            {
                throw new Exception("Parents are already full");
            }
        }

        public void AddChild(Person person)
        {
            Children ??= new List<Person>();

            if (Children.Contains(person)) throw new Exception("Child already present");

            Children.Add(person);
        }

        public IEnumerable<Person> GetPaternalUncles()
        {
            // Get all siblings except the person himself.
            var uncles = Father?.Relationships.GetSiblings()?.Where(x => x.Gender == Gender.Male)
                .Where(x => x != Father);
            return uncles;
        }

        public IEnumerable<Person> GetMaternalUncles()
        {
            var uncles = Mother?.Relationships.GetSiblings()?.Where(x => x.Gender == Gender.Male);
            return uncles;
        }

        public IEnumerable<Person> GetPaternalAunts()
        {
            var aunts = Father?.Relationships.GetSiblings()?.Where(x => x.Gender == Gender.Female);
            return aunts;
        }

        public IEnumerable<Person> GetMaternalAunts()
        {
            // Get all siblings except the person himself.
            return Mother?.Relationships.GetSiblings()?.Where(x => x.Gender == Gender.Female).Where(x => x != Mother);
        }

        public IEnumerable<Person> GetSistersInLaw()
        {
            var sil = new List<Person>();

            var spouseFemaleSiblings =
                Spouse?.First().Relationships.GetSiblings()?.Where(x => x.Gender == Gender.Female);

            if (spouseFemaleSiblings?.Any() == true)
                sil.AddRange(spouseFemaleSiblings);

            var maleSiblings = Siblings?.Where(x => x.Gender == Gender.Male) ?? Enumerable.Empty<Person>();
            foreach (var motherMaleChild in maleSiblings)
            {
                var spouse = motherMaleChild.Relationships.GetSpouse();

                if (spouse?.Any() == true)
                    sil.Add(spouse.First());
            }

            return sil;
        }

        public IEnumerable<Person> GetBrothersInLaw()
        {
            var bil = new List<Person>();

            var spouseMaleSiblings =
                Spouse?.First().Relationships.GetSiblings()
                    ?.Where(x => x.Gender == Gender.Male);

            if (spouseMaleSiblings?.Any() == true)
                bil.AddRange(spouseMaleSiblings);

            var femaleSiblings = Siblings?.Where(x => x.Gender == Gender.Female) ?? Enumerable.Empty<Person>();
            foreach (var motherFemaleChild in femaleSiblings)
            {
                var spouse = motherFemaleChild.Relationships.GetSpouse();

                if (spouse?.Any() == true)
                    bil.Add(spouse.First());
            }

            return bil;
        }

        public IEnumerable<Person> GetSon()
        {
            return Children?.Where(x => x.Gender == Gender.Male);
        }

        public IEnumerable<Person> GetDaughter()
        {
            return Children?.Where(x => x.Gender == Gender.Female);
        }

        public IEnumerable<Person> GetSiblings()
        {
            return Siblings;
        }

        public IEnumerable<Person> GetSpouse()
        {
            return Spouse;
        }

        public IEnumerable<Person> GetChildren()
        {
            return Children;
        }
    }
}