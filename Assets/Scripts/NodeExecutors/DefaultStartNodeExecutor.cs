using UnityEngine;
using UnityEngine.UI;
using DialogGraph;

public class DefaultStartNodeExecutor : NodeExecutorMediator<StartNode>
{
    public TMPro.TextMeshProUGUI ActNumText;

    public TMPro.TextMeshProUGUI ActNameText;

    public override void Execute(StartNode node)
    {
        Debug.Log("start node");
        ActNumText.text = node.ActNum.ToString();
        ActNameText.text = node.ActName;
    }
}
