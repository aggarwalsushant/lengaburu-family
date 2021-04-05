using System;
using System.IO;

namespace geektrust.FileElements
{
    public class TextFileParser : AbstractFileParser
    {
        public override void ParseFile(string path)
        {
            var userInputs = File.ReadAllLines(path);
            foreach (var input in userInputs)
            {
                var inputText = input.Split(' ');
                Enum.TryParse(inputText[0], out Command command);

                switch (command)
                {
                    case Command.ADD_CHILD:
                        ParseAddChildInput(inputText);
                        break;
                    case Command.GET_RELATIONSHIP:
                        ParseGetRelationshipInput(inputText);
                        break;
                }
            }
        }

        public void ParseAddChildInput(string[] input)
        {
            var parent = input[1];
            var childName = input[2];
            var childGender = input[3];
            Inputs.Add(Tuple.Create(Command.ADD_CHILD, parent, childName, childGender));
        }

        public void ParseGetRelationshipInput(string[] input)
        {
            var person = input[1];
            var relation = input[2];
            Inputs.Add(Tuple.Create(Command.GET_RELATIONSHIP, person, relation, string.Empty));
        }
    }
}