using UnityEngine;
using DialogGraph;

public class DefaultSetLandscapeExecutor : NodeExecutorMediator<SetLandscape>
{

    public override void Execute(SetLandscape node)
    {
        Debug.Log("set landscape node");

    }
}
