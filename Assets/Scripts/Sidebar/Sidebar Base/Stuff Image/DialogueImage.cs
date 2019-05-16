using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueImage : StuffImage
{
    public Dialogue DialoguePrefabs { get; set; }
    public string Content { get; set; }

    protected override void OnPointerClick()
    {
        var dialogue = Instantiate(DialoguePrefabs);
        dialogue.SetText(Content);

        StageMaster.Instance.Add(dialogue);
    }
}
