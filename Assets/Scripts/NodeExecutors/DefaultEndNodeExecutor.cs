using UnityEngine;
using DialogGraph;

public class DefaultEndNodeExecutor : NodeExecutorMediator<EndNode>
{

    public override void Execute(EndNode node)
    {
        Debug.Log("end node");
    }
}
