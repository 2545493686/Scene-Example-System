using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueImage : ImageGrid
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
            var dialogue = DialogueFactory.Instance.Instantiate(new DialogueFactory.InstantiateData
            {
                dialogueContents = new DialogueContent[]
                {
                    new DialogueContent
                    {
                        title = Data.GetRealName(),
                        content = Content
                    }
                },
                worldPoint = SceneMaster.Point
            });
            SceneMaster.Instance.Add(dialogue);
            dialogue.SetSelectedStuff();
        }
    }
}
