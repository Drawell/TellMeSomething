using UnityEngine;
using DialogGraph;

public class DefaultChoiseExecutor : NodeExecutorMediator<Choise>
{

    public override void Execute(Choise node)
    {
        Debug.Log("Choise node");
    }
}
