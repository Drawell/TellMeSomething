using UnityEngine;
using DialogGraph;

public class DefaultSwitchByItemExecutor : NodeExecutorMediator<SwitchByItem>
{

    public override void Execute(SwitchByItem node)
    {
        Debug.Log("switch by item node");

    }
}

