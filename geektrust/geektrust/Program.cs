using geektrust.Entities;
using geektrust.FileElements;
using System;
using System.Linq;

namespace geektrust
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args?.Any() == false)
                return;

            var lineage = new Lineage();
            var family = lineage.Family;

            var fileParser = new TextFileParser();
            fileParser.ParseFile(args?[0]);

            if (fileParser.Inputs.Count == 0)
                return;

            foreach (var input in fileParser.Inputs)
            {
                try
                {
                    switch (input.Item1)
                    {
                        case Command.ADD_CHILD:
                            AddChildOperation(input);
                            break;

                        case Command.GET_RELATIONSHIP:
                            GetRelationshipOperation(input);
                            break;

                        default:
                            throw new Exception("Invalid Action");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + Environment.NewLine);
                    //Console.WriteLine(e.StackTrace);
                }
            }

            void AddChildOperation(Tuple<Command, string, string, string> input)
            {
                Enum.TryParse(input.Item4, true, out Gender gender);
                lineage.AddChild(input.Item2, input.Item3, gender);
                Console.WriteLine("CHILD_ADDITION_SUCCEEDED");
            }

            void GetRelationshipOperation(Tuple<Command, string, string, string> input)
            {
                var members = family.FindRelatedMembers(input.Item2, input.Item3);
                if (members == null || !members.Any())
                {
                    Console.WriteLine("None");
                }
                else
                {
                    foreach (var person in members) Console.Write($"{person.Name} ");
                    Console.WriteLine();
                }
            }
        }
    }
}