using UnityEngine;
using UnityEngine.UI;
using DialogGraph;

public class DefaultReplicaExecutor : NodeExecutorMediator<Replica>
{
    public TMPro.TextMeshProUGUI TextHolder;
    public GameObject TextPanel;

    public override void Execute(Replica node)
    {
        TextPanel.SetActive(true);
        TextHolder.text = node.ReplicaText;
    }
}
