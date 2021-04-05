namespace geektrust.Entities
{
    public static class Womb
    {
        public static Person CreatePerson(string name, Gender gender)
        {
            return new Person(name, gender);
        }
    }
}