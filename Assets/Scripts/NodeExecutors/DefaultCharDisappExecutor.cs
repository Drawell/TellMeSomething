using UnityEngine;
using DialogGraph;

public class DefaultCharDisappExecutor : NodeExecutorMediator<CharacterDisappearance>
{

    public GameObject CharPanel;
    public override void Execute(CharacterDisappearance node)
    {
        Debug.Log("char disapp node");
        CharPanel.SetActive(false);

    }
}
