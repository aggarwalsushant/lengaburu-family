using System;
using System.Collections.Generic;

namespace geektrust.FileElements
{
    public abstract class AbstractFileParser
    {
        public List<Tuple<Command, string, string, string>> Inputs { get; } =
            new List<Tuple<Command, string, string, string>>();

        public abstract void ParseFile(string path);
    }
}