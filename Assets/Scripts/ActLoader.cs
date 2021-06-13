using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DialogGraph;

public static class ActLoader
{
    public static TaleAct LoadAct(string actPath,
        NodeExecutorMediator<StartNode> startNodeExcecutor,
        NodeExecutorMediator<Replica> replicaExecutor,
        NodeExecutorMediator<EndNode> endNodeExecutor)
    {
        TaleAct taleAct = new TaleAct();
        if (File.Exists(actPath))
        {
            string content = File.ReadAllText(actPath);

            SaveTaleActObject savedTaleAct = JsonUtility.FromJson<SaveTaleActObject>(content);
            taleAct.TaleName = savedTaleAct.TaleName;
            taleAct.ActId = savedTaleAct.ActId;

            foreach (StartNode startNode in savedTaleAct.StartNodes)
            {
                if (startNodeExcecutor == null)
                    throw new System.Exception("StartNodeExecutor is NULL!");

                startNode.SetExecutor(startNodeExcecutor);
                taleAct.StartNodes.Add(startNode.Id, startNode);
            }

            ActLoader.AddNodesToHeap(taleAct, savedTaleAct.Replicas, replicaExecutor);
            ActLoader.AddNodesToHeap(taleAct, savedTaleAct.EndNodes, endNodeExecutor);

            taleAct.CreateNodesConnections();
            return taleAct;
        }
        else
        {
            throw new System.Exception($"No such file {actPath}");
        }
    }

    private static void AddNodesToHeap(TaleAct taleAct, IEnumerable<Node> nodes, INodeExecutor executor)
    {
        foreach (Node node in nodes)
        {
            if (executor == null)
                throw new System.Exception($"Executor {node.GetType().ToString()} is NULL!");

            node.SetExecutor(executor);
            taleAct.NodeHeap.Add(node.Id, node);
        }
    }

    public class SaveTaleActObject
    {
        public string TaleName;
        public int ActId;
        public StartNode[] StartNodes;
        public Replica[] Replicas;
        public EndNode[] EndNodes;

    }
}
