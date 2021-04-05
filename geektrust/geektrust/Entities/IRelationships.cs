namespace geektrust.Entities
{
    public interface IRelationships
    {
        void AddSibling(Person person);
        void AddSpouse(Person person);
        void AddParent(Person person);
        void AddChild(Person person);
    }
}