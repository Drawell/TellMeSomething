using System.Collections.Generic;

namespace DialogGraph
{
    public class TaleAct
    {
        public string TaleName;
        public int ActId;

        private Dictionary<int, Character> _characters = new Dictionary<int, Character>();
        public Dictionary<int, Character> Characters
        {
            get => _characters;
        }
        private Dictionary<int, StartNode> _startNodes = new Dictionary<int, StartNode>();
        public Dictionary<int, StartNode> StartNodes
        {
            get => _startNodes;
        }

        private Dictionary<int, Node> _nodeHeap = new Dictionary<int, Node>();
        public Dictionary<int, Node> NodeHeap
        {
            get => _nodeHeap;
        }

        public TaleAct()
        {

        }

        public void CreateNodesConnections()
        {
            foreach (StartNode node in _startNodes.Values)
            {
                CreateConnectionForNode(node);
            }
            foreach (Node node in _nodeHeap.Values)
            {
                CreateConnectionForNode(node);
            }
        }

        private void CreateConnectionForNode(Node node)
        {
            foreach (int childId in node.ChildNodesId)
            {
                if (_nodeHeap.ContainsKey(childId))
                {
                    node.AddNextNode(_nodeHeap[childId]);
                }
                else
                {
                    throw new System.Exception($"Node with id {childId} not found!");
                }
            }
        }

        public StartNode GetStartNode(int startNodeId = 0)
        {
            if (_startNodes.ContainsKey(startNodeId))
            {
                return _startNodes[0];
            }
            else
            {
                throw new System.Exception($"No such start node: {startNodeId}");
            }

        }

    }
}