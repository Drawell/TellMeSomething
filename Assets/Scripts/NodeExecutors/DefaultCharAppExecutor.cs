using UnityEngine;
using DialogGraph;

public class DefaultCharAppExecutor : NodeExecutorMediator<CharacterAppearance>
{

    public override void Execute(CharacterAppearance node)
    {
        Debug.Log("charapp node");
    }
}
