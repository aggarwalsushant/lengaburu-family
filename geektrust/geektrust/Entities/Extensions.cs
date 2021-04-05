namespace geektrust.Entities
{
    public static class Extensions
    {
        public static string Cleanse(this string input)
        {
            return input.Trim().ToUpper();
        }
    }
}