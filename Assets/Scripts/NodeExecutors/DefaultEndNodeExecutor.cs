using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DialogGraph;

public class DefaultEndNodeExecutor : NodeExecutorMediator<EndNode>
{
    public GameObject EndPanel;
    public override void Execute(EndNode node)
    {
        Debug.Log("end node");
        EndPanel.SetActive(true);
        StartCoroutine(LoadMenu());

    }

    protected IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

}
