using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DialogGraph;
using System;

public static class ActLoader
{
    public static TaleAct LoadAct(string act,
        NodeExecutorMediator<StartNode> startNodeExcecutor,
        NodeExecutorMediator<CharacterAppearance> charAppExecutor,
        NodeExecutorMediator<CharacterDisappearance> charDisappExeutor,
        NodeExecutorMediator<Choice> choiseExecutor,
        NodeExecutorMediator<Replica> replicaExecutor,
        NodeExecutorMediator<AddItemNode> addItemNodeExecutor,
        NodeExecutorMediator<SwitchByItem> swithcByItemExecutor,
        NodeExecutorMediator<SetLandscape> setLandscapeExecutor,
        NodeExecutorMediator<EndNode> endNodeExecutor
        )
    {
        TaleAct taleAct = new TaleAct();
        string content = act;

        SaveTaleActObject savedTaleAct = JsonUtility.FromJson<SaveTaleActObject>(content);
        taleAct.TaleName = savedTaleAct.TaleName;
        taleAct.ActId = savedTaleAct.ActId;

        foreach (Character character in savedTaleAct.Characters)
        {
            taleAct.Characters.Add(character.CharId, character);
        }

        foreach (StartNode startNode in savedTaleAct.StartNodes)
        {
            if (startNodeExcecutor == null)
                throw new System.Exception("StartNodeExecutor is NULL!");

            startNode.SetExecutor(startNodeExcecutor);
            taleAct.StartNodes.Add(startNode.Id, startNode);
        }

        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.CharacterAppearances, charAppExecutor);
        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.CharacterDisappearances, charDisappExeutor);
        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.Choices, choiseExecutor);
        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.Replicas, replicaExecutor);
        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.AddItemNodes, addItemNodeExecutor);
        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.SwitchByItems, swithcByItemExecutor);
        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.SetLandscapes, setLandscapeExecutor);
        ActLoader.AddNodesToHeap(taleAct, savedTaleAct.EndNodes, endNodeExecutor);

        ActLoader.FindAndSetCharacter(taleAct, savedTaleAct.CharacterAppearances);
        ActLoader.FindAndSetCharacter(taleAct, savedTaleAct.CharacterDisappearances);
        ActLoader.FindAndSetCharacter(taleAct, savedTaleAct.Replicas);

        taleAct.CreateNodesConnections();
        return taleAct;
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

    private static void FindAndSetCharacter(TaleAct taleAct, CharacteredNode[] nodes)
    {
        foreach (CharacteredNode node in nodes)
        {
            if (taleAct.Characters.ContainsKey(node.CharacterId))
            {
                node.CharacterInstance = taleAct.Characters[node.CharacterId];
            }
            else
            {
                throw new Exception($"No such character {node.CharacterId}");
            }
        }
    }
    public class SaveTaleActObject
    {
        public string TaleName;
        public int ActId;

        public Character[] Characters;
        public StartNode[] StartNodes;
        public CharacterAppearance[] CharacterAppearances;
        public CharacterDisappearance[] CharacterDisappearances;
        public Choice[] Choices;
        public Replica[] Replicas;
        public AddItemNode[] AddItemNodes;
        public SwitchByItem[] SwitchByItems;
        public SetLandscape[] SetLandscapes;
        public EndNode[] EndNodes;

    }
}
