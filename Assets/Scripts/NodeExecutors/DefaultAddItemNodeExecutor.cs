using UnityEngine;
using DialogGraph;

public class DefaultAddItemNodeExecutor : NodeExecutorMediator<AddItemNode>
{

    public override void Execute(AddItemNode node)
    {
        Debug.Log("add item node");
    }
}
