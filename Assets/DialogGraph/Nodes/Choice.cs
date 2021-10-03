using System;
using System.Collections.Generic;

namespace DialogGraph
{
    [Serializable()]
    public class Choice : Node
    {
        public List<string> Choices;

    }
}