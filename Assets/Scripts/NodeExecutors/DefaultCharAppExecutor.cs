using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DialogGraph;
using System;

public class DefaultCharAppExecutor : NodeExecutorMediator<CharacterAppearance>
{
    public List<string> CharacterIds;
    public List<Sprite> CharacterSprites;
    public GameObject CharPanel;
    public Image UiImage;

    public override void Execute(CharacterAppearance node)
    {
        Debug.Log("char app node");
        UiImage.fillMethod = Image.FillMethod.Horizontal;

        CharPanel.SetActive(true);

        int charIdx = CharacterIds.IndexOf(node.CharacterId);
        if (charIdx != -1)
        {
            UiImage.sprite = CharacterSprites[charIdx];
        }
        else
        {
            throw new Exception($"There is no sprite for character {node.CharacterId}");
        }

    }
}
