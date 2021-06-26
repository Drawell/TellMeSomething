using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogGraph;
using System;
using TMPro;

public class DefaultChoiseExecutor : NodeExecutorMediator<Choise>
{
    public GameObject ChoisePanel;
    public List<GameObject> ChoiseButtons;

    public override void Execute(Choise node)
    {
        Debug.Log("Choise node");

        ChoisePanel.SetActive(true);
        int idx = 0;

        foreach (string choise in node.Choises)
        {
            if (ChoiseButtons.Count <= idx)
                throw new Exception("Need more choise buttons");

            ChoiseButtons[idx].SetActive(true);
            GameObject textObj = ChoiseButtons[idx].transform.GetChild(0).gameObject;
            TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
            text.text = choise;
            idx++;
        }

        for (int i = idx; i < ChoiseButtons.Count; i++)
        {
            ChoiseButtons[i].SetActive(false);
        }

    }
}
