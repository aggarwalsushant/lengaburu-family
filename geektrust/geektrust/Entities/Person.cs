using System;

namespace geektrust.Entities
{
    public class Person
    {
        public readonly IExtendedRelationships Relationships = new Relationships();

        public Person(string name, Gender gender)
        {
            Name = name;
            Gender = gender;

            Timestamp = DateTime.UtcNow;
        }

        public string Name { get; }
        public Gender Gender { get; }

        public DateTime Timestamp { get; }
    }
}