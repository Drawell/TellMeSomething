using UnityEngine;
using DialogGraph;

public abstract class NodeExecutorMediator<T> : MonoBehaviour, INodeExecutor where T : Node
{

    public void Execute(Node node)
    {
        this.Execute((T)node);
    }

    public abstract void Execute(T node);
}