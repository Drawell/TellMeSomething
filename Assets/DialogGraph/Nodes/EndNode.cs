
using System;

namespace DialogGraph
{

    [Serializable()]
    public class EndNode : Node
    {
        public int EndingId;

        override public Node GetNextNode(int nodeIdx = 0)
        {
            return Node.EmptyNode;
        }

    }

}