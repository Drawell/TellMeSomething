using System;
using System.Collections.Generic;

namespace DialogGraph
{

    [Serializable()]
    public class Node
    {
        public static readonly Node EmptyNode = new Node(new EmptyExecutor());

        private class EmptyExecutor : INodeExecutor
        {
            public void Execute(Node node)
            {
            }
        }

        public int Id;
        public float InitialDelay;
        public float AutoSkipDelay;
        public int[] NextNodesId;

        protected INodeExecutor Executor;

        [NonSerialized()]
        protected List<Node> NextNodes = new List<Node>();

        public Node()
        {

        }

        private Node(INodeExecutor executor)
        {
            Executor = executor;
        }


        public void SetExecutor(INodeExecutor executor)
        {
            this.Executor = executor;

        }

        public void Execute()
        {
            this.Executor.Execute(this);
        }

        public void AddNextNode(Node nextNode)
        {
            NextNodes.Add(nextNode);
        }

        public virtual Node GetNextNode(int nodeIdx = 0)
        {
            if (NextNodes.Count == 0)
            {
                throw new System.Exception("NextNodes is empty!");
            }
            else if (nodeIdx >= NextNodes.Count)
            {
                throw new System.Exception("Next node idx is out of range!");
            }
            else
            {
                return NextNodes[nodeIdx];
            }
        }
    }

}