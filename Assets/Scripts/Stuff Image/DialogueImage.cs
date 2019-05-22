using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueImage : StuffImage
{
    public DialogueContainer DialoguePrefabs { get; set; }
    public string Content { get; set; }

    protected override void OnPointerClick()
    {
        if (Stuff.SelectedStuff && Stuff.SelectedStuff.GetType() == typeof(DialogueContainer))
        {
            ((DialogueContainer)Stuff.SelectedStuff).AddDialogue(Data.GetRealName(), Content);
        }
        else
        {
            var dialogue = Instantiate(DialoguePrefabs);
            dialogue.AddDialogue(Data.GetRealName(), Content);
            StageMaster.Instance.Add(dialogue);
            Stuff.SelectedStuff = dialogue;
        }
    }
}
