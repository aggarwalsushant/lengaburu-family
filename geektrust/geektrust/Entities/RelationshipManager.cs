using System.Linq;

namespace geektrust.Entities
{
    public class RelationshipManager
    {
        public void AddParentChildSiblingRelationships(Person father, Person mother, Person child)
        {
            // Update relationships
            AddSiblingRelationships(mother, child);

            child?.Relationships.AddParent(mother);
            child?.Relationships.AddParent(father);

            mother?.Relationships.AddChild(child);
            father?.Relationships.AddChild(child);
        }

        public void AddSiblingRelationships(Person mother, Person child)
        {
            if (child == null) return;

            // Update relationships
            var allChildrenExceptNewChild = mother?.Relationships.GetChildren()?.Where(x => x != child) ??
                                            Enumerable.Empty<Person>();

            foreach (var person in allChildrenExceptNewChild)
            {
                person.Relationships.AddSibling(child);
                child.Relationships.AddSibling(person);
            }
        }

        public void AddSpouseRelationships(Person father, Person mother)
        {
            father?.Relationships.AddSpouse(mother);
            mother?.Relationships.AddSpouse(father);
        }
    }
}