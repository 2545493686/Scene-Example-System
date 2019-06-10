using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : Stuff
{
    public DialogueContainer DialogueContainer { set; get; }
    public int Index { set; get; }

    public virtual void SetText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }
}
