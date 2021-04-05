using System;
using System.Collections.Generic;
using System.Linq;

namespace geektrust.Entities
{
    public class Lineage
    {
        private readonly RelationshipManager _relationManager;
        private readonly Dictionary<string, Person> _members;

        public Lineage()
        {
            _relationManager = new RelationshipManager();
            _members = new Dictionary<string, Person>();
            InitialiseFamilyTree(_members);
            Family = new Family(_members);
        }

        public IFamily Family { get; }

        private void InitialiseFamilyTree(IDictionary<string, Person> members)
        {
            members.Add("King Shan", Womb.CreatePerson("King Shan", Gender.Male));
            members.Add("Queen Anga", Womb.CreatePerson("Queen Anga", Gender.Female));

            var king = members["King Shan"];
            var queen = members["Queen Anga"];
            _relationManager.AddSpouseRelationships(king, queen);

            AddChild(queen.Name, "Chit", Gender.Male);
            AddChild(queen.Name, "Ish", Gender.Male);
            AddChild(queen.Name, "Vich", Gender.Male);
            AddChild(queen.Name, "Aras", Gender.Male);
            AddChild(queen.Name, "Satya", Gender.Female);

            // Level 1 clear
            AddSpouse("Chit", "Amba", Gender.Female);
            AddSpouse("Vich", "Lika", Gender.Female);
            AddSpouse("Aras", "Chitra", Gender.Female);
            AddSpouse("Satya", "Vyan", Gender.Male);

            // Adding chit amba children
            var mother = members["Amba"];
            AddChild(mother.Name, "Dritha", Gender.Female);
            AddChild(mother.Name, "Tritha", Gender.Female);
            AddChild(mother.Name, "Vritha", Gender.Male);

            members["Dritha"].Relationships.AddSpouse(Womb.CreatePerson("Jaya", Gender.Male));

            // Adding vich lika children
            mother = members["Lika"];
            AddChild(mother.Name, "Vila", Gender.Female);
            AddChild(mother.Name, "Chika", Gender.Female);

            //Adding aras and chitra children
            mother = members["Chitra"];
            AddChild(mother.Name, "Jnki", Gender.Female);
            AddChild(mother.Name, "Ahit", Gender.Male);
            AddSpouse("Jnki", "Arit", Gender.Male);

            //Adding satya and vyan children
            mother = members["Satya"];
            AddChild(mother.Name, "Atya", Gender.Female);
            AddChild(mother.Name, "Asva", Gender.Male);
            AddChild(mother.Name, "Vyas", Gender.Male);
            AddSpouse("Asva", "Satvy", Gender.Female);
            AddSpouse("Vyas", "Krpi", Gender.Female);

            //Adding dritha and jaya children
            mother = members["Dritha"];
            AddChild(mother.Name, "Yodhan", Gender.Male);

            //Adding arit and jnki children
            mother = members["Jnki"];
            AddChild(mother.Name, "Lavnya", Gender.Female);
            AddChild(mother.Name, "Laki", Gender.Male);

            //Adding satvy and asva children
            mother = members["Satvy"];
            AddChild(mother.Name, "Vasa", Gender.Male);

            //Adding krpi and vyas children
            mother = members["Krpi"];
            AddChild(mother.Name, "Krithi", Gender.Female);
            AddChild(mother.Name, "Kriya", Gender.Male);
        }

        public bool AddChild(string parentName, string childName, Gender gender)
        {
            if (_members.ContainsKey(childName))
                throw new Exception("Family member with same name already exists.");

            _members.TryGetValue(parentName, out var mother);

            if (mother == null)
                throw new Exception("PERSON_NOT_FOUND");

            // Confirming she is a MOTHER or not.
            if (mother.Gender == Gender.Male)
                throw new Exception("Child Addition Failed");

            var child = Womb.CreatePerson(childName, gender);
            var father = mother.Relationships.GetSpouse()?.First();

            if (mother.Gender == father?.Gender) throw new Exception("cannot add child via same gender parents");

            _relationManager.AddParentChildSiblingRelationships(father, mother, child);
            _members.Add(childName, child);
            return true;
        }

        public bool AddSpouse(string partner, string spouse, Gender gender)
        {
            if (_members.TryGetValue(partner, out var partnerObj))
            {
                var spouseObj = Womb.CreatePerson(spouse, gender);
                _relationManager.AddSpouseRelationships(partnerObj, spouseObj);
                _members.Add(spouse, spouseObj);
                return true;
            }

            return false;
        }
    }
}