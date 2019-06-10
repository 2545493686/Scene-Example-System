using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditableDialogue : Dialogue
{
    public override void SetText(string text)
    {
        GetComponentInChildren<InputField>().text = text;
    }

    public void SetContent(string text)
    {
        Debug.Log("set text:" + text);
        DialogueContainer.SetContent(Index, text);
    }
}
