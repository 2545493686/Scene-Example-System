using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFactory : StuffFactoryBase<DialogueFactory, DialogueFactory.InstantiateData>
{
    public DialogueContainer dialoguePrefabs;

    public struct InstantiateData
    {
        public DialogueContent[] dialogueContents;
        public string containerTitle;
        public Vector3 worldPoint;
    }

    public override Stuff Instantiate(InstantiateData instantiateData)
    {
        var dialogue = Instantiate(dialoguePrefabs);
        foreach (var contents in instantiateData.dialogueContents)
        {
            dialogue.AddDialogue(contents.title, contents.content);
        }
        dialogue.SetTitle(instantiateData.containerTitle);
        dialogue.transform.position = instantiateData.worldPoint;
        dialogue.Factory = this;
        return dialogue;
    }
}
